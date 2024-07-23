using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public static StorageManager Instance { get; set; }

    [SerializeField] GameObject storageBoxSmallUI;
    [SerializeField] StorageBox selectedStorage;
    public bool storageUIOpen;

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

    public void OpenBox(StorageBox storage)
    {
        SetSelectedStorage(storage);

        PopulateStorage(GetRelevantUI(selectedStorage));

        GetRelevantUI(selectedStorage).SetActive(true);
        storageUIOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        InteractionManager.Instance.DisableSelection();
        InteractionManager.Instance.GetComponent<InteractionManager>().enabled = false;
    }

    private void PopulateStorage(GameObject storageUI)
    {
        // Get all slots of the ui
        List<GameObject> uiSlots = new List<GameObject>();

        foreach (Transform child in storageUI.transform)
        {
            uiSlots.Add(child.gameObject);
        }

        // Now, instantiate the prefab and set it as a child of each GameObject
        foreach (string name in selectedStorage.items)
        {
            foreach (GameObject slot in uiSlots)
            {
                if (slot.transform.childCount < 1)
                {
                    var itemToAdd = Instantiate(Resources.Load<GameObject>(name), slot.transform.position, slot.transform.rotation);
                    itemToAdd.transform.SetParent(slot.transform);
                    break;
                }
            }
        }
    }

    public void CloseBox()
    {
        RecaculateStorageBox(GetRelevantUI(selectedStorage));
        GetRelevantUI(selectedStorage).SetActive(false);
        storageUIOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InteractionManager.Instance.EnableSelection();
        InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
    }

    private void RecaculateStorageBox(GameObject storageUI)
    {
        List<GameObject> uiSlots = new List<GameObject>();
        foreach (Transform child in storageUI.transform)
        {
            uiSlots.Add(child.gameObject);
        }

        // clear List item

        selectedStorage.items.Clear();
        List<GameObject> tobeDeleted = new List<GameObject>();

        // take  the inventory items and convert to string

        foreach (GameObject slot in uiSlots)
        {
            if (slot.transform.childCount > 0)
            {
                // remove clone
                string name = slot.transform.GetChild(0).name;
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");
                selectedStorage.items.Add(result);
                tobeDeleted.Add(slot.transform.GetChild(0).gameObject);
            }
        }

        foreach(GameObject obj in tobeDeleted)
        {
            Destroy(obj);
        }
    }

    public void SetSelectedStorage(StorageBox storage)
    {
        selectedStorage = storage;
    }

    private GameObject GetRelevantUI(StorageBox storage)
    {
        // Create a switch for other types
        return storageBoxSmallUI;
    }
}

