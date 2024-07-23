using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public bool isSavingToJson;
    public bool isLoading;

    [Header("Loading Scene")]
    [SerializeField] private string SceneName;

    // Json Project Save Path
    string JsonPathProject;
    // Json External/Real Save Path

    string JsonPathPersistant;
    // Binary Save path
    string BinaryPath;

    // File Name    
    string fileName = "SaveGame";
    public static SaveManager Instance { get; set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        JsonPathProject = Application.dataPath + Path.AltDirectorySeparatorChar;

        // tao 1 file save vao trong save_game.bin, co the thay doi ten file

        JsonPathPersistant = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
        BinaryPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
    }

    #region || --------- GENERAL SECTION ----------||




    #endregion

    #region || --------- SAVING SECTION ----------||

    public void SaveGame(int slotNumber)
    {
        AllGameData data = new AllGameData();

        data.playerdata = GetPlayerData();

        data.environmentData = GetEnvironmentData();

        SavingTypeSwitch(data, slotNumber);
    }

    private EnvironmentData GetEnvironmentData()
    {
        List<string> itemPickuped = InventorySystem.Instance.itemsPickedup;
        return new EnvironmentData(itemPickuped);
    }

    private PlayerData GetPlayerData()
    {

        int[] playerStats = new int[5];

        playerStats[0] = PlayerStatusManager.Instance.playerdataSo.CurrentHP;
        playerStats[1] = PlayerStatusManager.Instance.playerdataSo.CurrentEnegy;
        playerStats[2] = PlayerStatusManager.Instance.playerdataSo.CurrentEXP;
        playerStats[3] = PlayerStatusManager.Instance.playerdataSo.CurrentCoin;
        playerStats[4] = PlayerStatusManager.Instance.playerdataSo.playerLevel;

        float[] playerPosAndRot = new float[6];

        playerPosAndRot[0] = PlayerStatusManager.Instance.playerBody.transform.position.x;
        playerPosAndRot[1] = PlayerStatusManager.Instance.playerBody.transform.position.y;
        playerPosAndRot[2] = PlayerStatusManager.Instance.playerBody.transform.position.z;

        playerPosAndRot[3] = PlayerStatusManager.Instance.playerBody.transform.rotation.x;
        playerPosAndRot[4] = PlayerStatusManager.Instance.playerBody.transform.rotation.y;
        playerPosAndRot[5] = PlayerStatusManager.Instance.playerBody.transform.rotation.z;

        //string[] playerName = new string[1];
        //playerName[0] = PlayerStatusManager.Instance.PlayerName;

        string[] inventory = InventorySystem.Instance.itemList.ToArray();
        string[] quickSlot = GetQuickSlotContent();
        return new PlayerData(playerStats, playerPosAndRot, inventory, quickSlot);
    }

    private string[] GetQuickSlotContent()
    {
        List<string> temp = new List<string>();
        foreach (GameObject slot in EquipManager.Instance.quickSlotsList)
        {
            if (slot.transform.childCount != 0)
            {
                string name = slot.transform.GetChild(0).name;
                string str2 = "(Clone)";
                string cleanName = name.Replace(str2, "");
                temp.Add(cleanName);
            }
        }
        return temp.ToArray();
    }

    public void SavingTypeSwitch(AllGameData data, int slotNumber)
    {
        if (isSavingToJson)
        {
            SaveGameToJsonFile(data, slotNumber);
        }
        else
        {
            SaveGameToBinaryFile(data, slotNumber);
        }
    }

    #endregion

    #region || --------- LOADING SECTION ----------||
    public AllGameData SelectedLoadingTypeSwitch(int slotNumber)
    {
        if (isSavingToJson)
        {
            AllGameData gameData = LoadGameFromJsonFile(slotNumber);
            return gameData;
        }
        else
        {
            AllGameData gameData = LoadGameFromBinaryFile(slotNumber);
            return gameData;
        }
    }

    // LOADING CAC TRANG THAI O DAY
    public void LoadGame(int slotNumber)
    {
        // PLAYER DATA
        SetPlayerData(SelectedLoadingTypeSwitch(slotNumber).playerdata);

        // ENVIRONMENT
        SetEnvirontmentData(SelectedLoadingTypeSwitch(slotNumber).environmentData);


        isLoading = false;
    }

    private void SetEnvirontmentData(EnvironmentData environmentData)
    {
        foreach (Transform itemType in EnvironmentManager.Instance.allItems.transform)
        {
            foreach (Transform item in itemType.transform)
            {
                if (environmentData.pickedUpItems.Contains(item.name))
                {
                    Destroy(item.gameObject);
                }
                foreach (Transform itemInside in item.transform)
                {
                    if (environmentData.pickedUpItems.Contains(itemInside.name))
                    {
                        Destroy(itemInside.gameObject);
                    }
                }
            }
        }

        InventorySystem.Instance.itemsPickedup = environmentData.pickedUpItems;
    }

    private void SetPlayerData(PlayerData playerdata)
    {
        // setting player Stats

        PlayerStatusManager.Instance.playerdataSo.CurrentHP = playerdata.playerStats[0];
        PlayerStatusManager.Instance.playerdataSo.CurrentEnegy = playerdata.playerStats[1];
        PlayerStatusManager.Instance.playerdataSo.CurrentEXP = playerdata.playerStats[2];
        PlayerStatusManager.Instance.playerdataSo.CurrentCoin = playerdata.playerStats[3];
        PlayerStatusManager.Instance.playerdataSo.playerLevel = playerdata.playerStats[4];

        // Player Position 

        Vector3 Loadposition;
        Loadposition.x = playerdata.PlayerPositionAndRotation[0];
        Loadposition.y = playerdata.PlayerPositionAndRotation[1];
        Loadposition.z = playerdata.PlayerPositionAndRotation[2];

        PlayerStatusManager.Instance.playerBody.transform.position = Loadposition;

        Vector3 Loadrotation;
        Loadrotation.x = playerdata.PlayerPositionAndRotation[3];
        Loadrotation.y = playerdata.PlayerPositionAndRotation[4];
        Loadrotation.z = playerdata.PlayerPositionAndRotation[5];

        PlayerStatusManager.Instance.playerBody.transform.rotation = Quaternion.Euler(Loadrotation);

        // Setting the inventory content 
        foreach (string item in playerdata.inventoryContent)
        {
            InventorySystem.Instance.AddToInventory(item);
        }

        // setting quickslot content
        foreach (string item in playerdata.quickSlotContent)
        {
            // Finding next free Slot
            GameObject availableslot = EquipManager.Instance.FindNextEmptySlot();

            var itemToAdd = Instantiate(Resources.Load<GameObject>(item));

            itemToAdd.transform.SetParent(availableslot.transform, false);
        }
        isLoading = false;
    }


    public void StartLoadedGame(int slotNumber)
    {
        isLoading = true;
        StartCoroutine(LoadingSceneManager.Instance.LoadSceneFromLoadData(SceneName, slotNumber));
    }







    #endregion

    #region || --------- TO BINARY SECTION ----------||

    public void SaveGameToBinaryFile(AllGameData data, int slotNumber)
    {
        BinaryFormatter formatter = new BinaryFormatter();


        FileStream stream = new FileStream(BinaryPath + fileName + slotNumber + ".bin", FileMode.Create); // creat file

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Data Save to " + BinaryPath + fileName + slotNumber + ".bin");
    }


    public AllGameData LoadGameFromBinaryFile(int slotNumber)
    {
        // check file nay co ton tai khong,
        if (File.Exists(BinaryPath + fileName + slotNumber + ".bin"))
        {

            BinaryFormatter Formatter = new BinaryFormatter();
            FileStream stream = new FileStream(BinaryPath + fileName + slotNumber + ".bin", FileMode.Open); // open file

            AllGameData data = Formatter.Deserialize(stream) as AllGameData;
            stream.Close(); // lay xong thi dong lai

            Debug.Log("Data Load From " + BinaryPath + fileName + slotNumber + ".bin");
            return data;
        }
        else
        {
            return null;
        }

    }
    #endregion

    #region || --------- TO JSON SECTION ----------||

    public void SaveGameToJsonFile(AllGameData data, int slotNumber)
    {
        string json = JsonUtility.ToJson(data);

        //string encryted = EncrytionDecrytion(json);
        using (StreamWriter writer = new StreamWriter(JsonPathProject + fileName + slotNumber + ".json"))
        {
            writer.Write(json);
            print("Saved game To json File at:" + JsonPathProject + fileName + slotNumber + ".json");
        };
    }


    public AllGameData LoadGameFromJsonFile(int slotNumber)
    {
        using (StreamReader reader = new StreamReader(JsonPathProject + fileName + slotNumber + ".json"))
        {
            string json = reader.ReadToEnd();

            // string decrypted = EncrytionDecrytion(json);

            AllGameData data = JsonUtility.FromJson<AllGameData>(json);
            return data;
        };
    }
    #endregion

    #region || --------- ENCRYTION  ----------||
    public string EncrytionDecrytion(string jsonString)
    {
        string keyWord = "1234567"; // cai nay nhu Password, Bao ve keyword that tot

        string result = "";

        for (int i = 0; i < jsonString.Length; i++)
        {
            result += (char)(jsonString[i] ^ keyWord[i % keyWord.Length]);
        }
        return result;

    }

    #endregion

    #region || ---------- Utility --------||

    public bool DoesFileExits(int slotNumber)
    {
        if (isSavingToJson)
        {
            if (System.IO.File.Exists(JsonPathProject + fileName + slotNumber + ".json"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (System.IO.File.Exists(BinaryPath + fileName + slotNumber + ".bin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void DeselectButton()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public bool isSlotEmpty(int slotNumber)
    {
        if (DoesFileExits(slotNumber))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region || ---------- SETTING SECTION ------------ ||
    #region || ---------- VOLUME SETTING  ------------ ||
    [System.Serializable]
    public class VolumeSettings
    {
        public float music;
        public float effect;
    }

    public void SavedVolumeSetting(float _music, float _effect)
    {
        VolumeSettings volumeSettings = new VolumeSettings()
        {
            music = _music,
            effect = _effect,         
        };

        PlayerPrefs.SetString("Volume", JsonUtility.ToJson(volumeSettings)); //  Save tu Json
        PlayerPrefs.Save();
        Debug.Log("Save to player Preb");
    }

    public VolumeSettings LoadVolumeSettings() // Load tu Json, tu class ma chung ta vua save.
    {
        return JsonUtility.FromJson<VolumeSettings>(PlayerPrefs.GetString("Volume")); // getring o tren, tu class Setstring
    }
    #endregion
    #endregion
}
