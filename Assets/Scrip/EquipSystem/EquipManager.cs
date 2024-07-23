using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();


    public GameObject numsberHolder;

    public int selectedNumber = -1;
    public GameObject selectedItem;
    [Header("Tool Holder")]
    public GameObject toolHolder;
    public GameObject selectedItemModel;
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


    private void Start()
    {
        PopulateSlotList();
    }

    private void Update()
    {
        inputDescription();
    }

    private void inputDescription()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7);
        }
    }

    private void SelectQuickSlot(int number)
    {
        if (checkIfSlotIsFull(number) == true)
        {
            if (selectedNumber != number)
            {
                selectedNumber = number;
                // unselect PReviously sl item
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isEquippedSeleted = false;
                }

                selectedItem = getSelectedItem(number);
                selectedItem.GetComponent<InventoryItem>().isEquippedSeleted = true;

                SetEquippedModel(selectedItem);

                // changing the color

                foreach (Transform child in numsberHolder.transform)
                {
                    child.transform.Find("Text").GetComponent<TextMeshProUGUI>().color = Color.grey;
                }

                TextMeshProUGUI tobeChange = numsberHolder.transform.Find("number" + number).transform.Find("Text").GetComponent<TextMeshProUGUI>();
                tobeChange.color = Color.white;
            }
            else // try to select the same slot 
            {
                selectedNumber = -1; // null

                // unselect PReviously sl item
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isEquippedSeleted = false;
                    selectedItem = null;
                }

                if (selectedItemModel != null)
                {

                    if (selectedItemModel.CompareTag("Guns"))
                    {

                        Guns weapon = selectedItemModel.GetComponent<Guns>();

                        switch (weapon.thisWeaponModel)
                        {
                            case Guns.WeaponModel.Sci_max:
                                TempBulletsLerftManager.Instance.SCI_bullets_afterDestroy = weapon.bulletsLeft;

                                break;
                            case Guns.WeaponModel.Akai_max:
                                TempBulletsLerftManager.Instance.Grenade_bullets_afterDestroy = weapon.bulletsLeft;
                                break;
                        }
                        DestroyImmediate(selectedItemModel.gameObject);
                        selectedItemModel = null;
                    }
                    else
                    {
                        DestroyImmediate(selectedItemModel.gameObject);
                        selectedItemModel = null;
                    }

                    // changing the color

                    foreach (Transform child in numsberHolder.transform)
                    {
                        child.transform.Find("Text").GetComponent<TextMeshProUGUI>().color = Color.grey;
                    }
                }
            }
        }
    }

    private void SetEquippedModel(GameObject selectedItem)
    {
        if (selectedItemModel != null)
        {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;
        }

        string selectedItemName = selectedItem.name.Replace("(Clone)", "");

        selectedItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_model"));
        //--------------------- CASE TOOL ---------------------------

        if (selectedItemModel.CompareTag("Tools"))
        {

            EquipableItem tools = selectedItemModel.GetComponent<EquipableItem>();
            InteractableObject toolsinterac = selectedItemModel.GetComponent<InteractableObject>();
            InventoryItem item = selectedItemModel.GetComponent<InventoryItem>();

            selectedItemModel.transform.localPosition = new Vector3(tools.spawnPosition.x, tools.spawnPosition.y, tools.spawnPosition.z);

            selectedItemModel.transform.localRotation = Quaternion.Euler(tools.spawnRotation.x, tools.spawnRotation.y, tools.spawnRotation.z);

            selectedItemModel.transform.localScale = tools.transform.localScale;

            selectedItemModel.gameObject.tag = "Weapon";
            Rigidbody rb = tools.GetComponent<Rigidbody>();

            //InteractionManager.Instance.timeDecaytext.text = "";
            GlobalReferences.Instance.isActiveEquipnbro = true;
            tools.isEquipableActive = true;
            tools.animator.enabled = true;
            toolsinterac.ItemName = "";
            toolsinterac.enabled = false;
            rb.isKinematic = true;


            selectedItemModel.transform.SetParent(toolHolder.transform, false);
        }
        //--------------------- CASE Guns ---------------------------
        if (selectedItemModel.CompareTag("Guns"))
        {
            InteractableObject toolsinterac = selectedItemModel.GetComponent<InteractableObject>();
            Guns weapon = selectedItemModel.GetComponent<Guns>();

            selectedItemModel.transform.localPosition = new Vector3(weapon.spawnPosition.x, weapon.spawnPosition.y, weapon.spawnPosition.z);

            selectedItemModel.transform.localRotation = Quaternion.Euler(weapon.spawnRotation.x, weapon.spawnRotation.y, weapon.spawnRotation.z);

            selectedItemModel.transform.localScale = weapon.transform.localScale;
            selectedItemModel.gameObject.tag = "Weapon";

            toolsinterac.enabled = false;
            toolsinterac.ItemName = "";
            GlobalReferences.Instance.isActiveWeaponbro = true;
            weapon.isActiveWeapon = true;
            // InteractionManager.Instance.timeDecaytext.text = "";
            Rigidbody rb = weapon.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            weapon.animator.enabled = true;

            switch (weapon.thisWeaponModel)
            {
                case Guns.WeaponModel.Sci_max:
                    weapon.bulletsLeft = TempBulletsLerftManager.Instance.SCI_bullets_afterDestroy;

                    break;
                case Guns.WeaponModel.Akai_max:
                    weapon.bulletsLeft = TempBulletsLerftManager.Instance.Grenade_bullets_afterDestroy;
                    break;
            }

            selectedItemModel.transform.SetParent(toolHolder.transform, false);
        }
    }

    GameObject getSelectedItem(int slotNumber)
    {
        return quickSlotsList[slotNumber - 1].transform.GetChild(0).gameObject;
    }

    private bool checkIfSlotIsFull(int Slotnumber)
    {
        if (quickSlotsList[Slotnumber - 1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        InventorySystem.Instance.ReCalculateList();
    }


    public GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}