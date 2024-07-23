using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    [Header("Inventory")]
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject WhatSlotToEquip;
    public GameObject ItemInfoUI;

    public GameObject inventoryScreenUI;
    public bool isInventoryOpen;


    // is Inventoryfull
    [SerializeField] private TextMeshProUGUI isFullText;

    [Header("PopUpItem Pickup")]
    // popup
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupName;
    public Image pickupImage;
    public List<string> itemsPickedup;


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
    }


    void Start()
    {
        isFullText.gameObject.SetActive(false);
        isInventoryOpen = false;
        inventoryScreenUI.SetActive(false);
        PopularSlotList();
       
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !ConstructionManager.Instance.inConstructionMode)
        {
            ToggleInventory();
        }
    }

    private void PopularSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }


    }

    private void ToggleInventory()
    {
        inventoryScreenUI.SetActive(!inventoryScreenUI.activeSelf);

        if (inventoryScreenUI.activeInHierarchy)
        {

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isInventoryOpen = true;
            InteractionManager.Instance.DisableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = false;
        }
        else
        {
            if (!CraftingManager.Instance.IsCraftOpen && !ShopManager.Instance.isShopOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                InteractionManager.Instance.EnableSelection();
                InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
            }

            isInventoryOpen = false;
        }
    }

    public void AddToInventory(string itemName)
    {
        if(!SaveManager.Instance.isLoading)
        {

        }
        WhatSlotToEquip = FindNextEmptySlot();
        itemToAdd = (GameObject)Instantiate(Resources.Load<GameObject>(itemName), WhatSlotToEquip.transform.position,
            WhatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(WhatSlotToEquip.transform);
        
        itemList.Add(itemName);
        SoundMainMenu.Instance.Play_PointerSFX();
        Sprite spritePopup = itemToAdd.GetComponent<Image>().sprite;
       
        StartCoroutine(triggerPopup(itemName, spritePopup));
       
        ReCalculateList();
        CraftingManager.Instance.RefreshNeededItems();

        QuestManager.Instance.RefreshTrackerQuestList();

    }

    private IEnumerator triggerPopup(string name, Sprite itemImage)
    {
        pickupAlert.SetActive(true);
        pickupName.text = name;
        pickupImage.sprite = itemImage;
        yield return new WaitForSeconds(3f);
        pickupAlert.SetActive(false);
        pickupName.text = "";
        pickupImage.sprite = null;
    }
   
    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }


        }
        if (counter == 40)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }


        }
        return new GameObject();
    }

    public void PlayInventoryFullEffect()
    {
        StartCoroutine(isFullInventoryText());
    }
    private IEnumerator isFullInventoryText()
    {
        if (isFullText.gameObject.activeInHierarchy == false)
        {
            isFullText.gameObject.SetActive(true);
        }

        var text = isFullText.GetComponent<TextMeshProUGUI>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = text.color;
        startColor.a = 1f;
        text.color = startColor;

        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = text.color;
            newColor.a = alpha;
            text.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (isFullText.gameObject.activeInHierarchy)
        {
            isFullText.gameObject.SetActive(false);
        }
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;
        for(var i = slotList.Count - 1; i >= 0; i--) // ham nay check toan bo slot co trong mang.
        {
            if (slotList[i].transform.childCount > 0)    // dung phuong phap nay check tu cao den thap cac slot co item hay khong
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0) // check xem cai gameobject nay co child nao khong. (item inside)  va check name + clone
                {
                    DestroyImmediate(slotList[i].transform.GetChild(0).gameObject); //  sau khi tim xong, destroy cai item object trong slot nay . luu y transform.
                    counter -= 1; //  tru so luong item
                }
            }
        }

        ReCalculateList();
        CraftingManager.Instance.RefreshNeededItems();
        QuestManager.Instance.RefreshTrackerQuestList();

    }

    public void ReCalculateList()
    {
        itemList.Clear(); //  clear toan bo list ngay tu dau, de tinh lai
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0) // kiem tra neu co item trong slot.
            {
                string name  = slot.transform.GetChild(0).name; // tim hieu ve getchild (0) : trong slot chi co 1 game object, vd: stonr (Clone)             
                string str2 = "(Clone)";
                string result = name.Replace(str2,"");

                itemList.Add(result);
            }
        }
    }

    public int CheckItemAmount(string name)
    {
        int itemCount = 0;
        foreach(string item in itemList)
        {
            if(item == name)
            {
                itemCount++;
            }
        }
        return itemCount;
    }
}