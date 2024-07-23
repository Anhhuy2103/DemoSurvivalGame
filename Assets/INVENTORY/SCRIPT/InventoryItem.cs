using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static InventoryItem;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // --- Is this item trashable --- //
    public bool isTrashable;

    // --- Item Info UI --- //
    private GameObject itemInfoUI;

    private TextMeshProUGUI itemInfoUI_itemName;
    private TextMeshProUGUI itemInfoUI_itemDescription;
    private TextMeshProUGUI itemInfoUI_itemFunctionality;
    private Text itemInfoUI_itemDurable;

    public string thisName, thisDescription, thisFunctionality;


    // --- Consumption --- //
    private GameObject itemPendingConsumption;
    public bool isConsumable;
    public bool isUseable;


    //--- Equipping --- //
    public bool isItemEquipable;
    private GameObject itemPendingEquipping;
    public bool isInsideQuickSlot;
    public bool isEquippedSeleted;
    public bool isTheSameItem = false;

    // -------- *** ITEM ENUM TYPE *** -------

    public enum EnumTypeOfItem
    {
        none = 0,
        HealPotion,
        EnegyPotion,
        CoinMoney,
        Weapon,
        Resources,
        Tatical,
        Lethal,
    }

    [Header("Item Data")]
    public EnumTypeOfItem thisTypeOfItems;
    [SerializeField] private int HPAmount;
    [SerializeField] private int EnegyAmount;
    [SerializeField] private int MoneyAmount;
    [SerializeField] private int itemCurrenHeal;
    [SerializeField] private int itemMaxHeal;



    //----------------------------------------------------------------
    private void Start()
    {
        itemInfoUI = InventorySystem.Instance.ItemInfoUI;
        itemInfoUI_itemName = itemInfoUI.transform.Find("itemName").GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("itemDescription").GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("itemFunctionality").GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemDurable = itemInfoUI.transform.Find("itemDurable").GetComponent<Text>();

    }

    private void Update()
    {
        if (isEquippedSeleted)
        {
            gameObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }
    // Triggered when the mouse enters into the area of the item that has this script.
    public void OnPointerEnter(PointerEventData eventData)
    {

        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;

        if (thisTypeOfItems == EnumTypeOfItem.Weapon)
        {
            itemInfoUI_itemDurable.gameObject.SetActive(true);
        }
        else
        {
            itemInfoUI_itemDurable.gameObject.SetActive(false);
        }
        itemMaxHeal = GlobalReferences.Instance.itemMaxtHP;
        itemCurrenHeal = GlobalReferences.Instance.itemCurrentHP;
        itemInfoUI_itemDurable.text = $"Durable:{itemCurrenHeal} / {itemMaxHeal}";

    }

    // Triggered when the mouse exits the area of the item that has this script.
    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }

    // Triggered when the mouse is clicked over the item that has this script.



    // ********** ------------ USE ITEM IN HERE BY RIGHT CLICK BUDDY ------------ ********
    public void OnPointerDown(PointerEventData eventData)
    {
        //Right Mouse Button Click on
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                // Setting this specific gameobject to be the item we want to destroy later
                itemInfoUI.SetActive(false);
                itemPendingConsumption = gameObject;
                if (thisTypeOfItems == EnumTypeOfItem.HealPotion)
                {
                    HealHPEffect(HPAmount);
                    SoundManager.Instance.Play_HealingSound();
                }
                if (thisTypeOfItems == EnumTypeOfItem.EnegyPotion)
                {
                    HealEnegyEffect(EnegyAmount);
                    SoundManager.Instance.Play_HealingSound();
                }
                if (thisTypeOfItems == EnumTypeOfItem.CoinMoney)
                {
                    IncreaseMoneyEffect(MoneyAmount);
                    SoundMainMenu.Instance.Play_PointerSFX();
                }
                if (thisTypeOfItems == EnumTypeOfItem.Tatical)
                {
                    AddTaticalBoom();
                    SoundMainMenu.Instance.Play_PointerSFX();
                }
                if (thisTypeOfItems == EnumTypeOfItem.Lethal)
                {
                    AddLethalBoom();
                    SoundMainMenu.Instance.Play_PointerSFX();
                }
            }

            // --- Equippable menthod --- //
            if (isItemEquipable && !isInsideQuickSlot && !EquipManager.Instance.CheckIfFull())
            {
                EquipManager.Instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
                isTheSameItem = true;
            }

            // ------ Item Useable ------ // 
            if (isUseable)
            {
               
                gameObject.SetActive(false);
                UseItem();
            }
        }


    }



    //*****************************************************************************
    // Triggered when the mouse button is released over the item that has this script.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {

                DestroyImmediate(gameObject);
                InventorySystem.Instance.ReCalculateList();
                CraftingManager.Instance.RefreshNeededItems();
            }
        }
    }

    // ------------ USE ITEM FUNCTION OVER HERE BUDDY <3 -------
    private void consumingFunction(int healAmount)
    {
        itemInfoUI.SetActive(false);
        if (thisTypeOfItems == EnumTypeOfItem.EnegyPotion)
        {
            HealEnegyEffect(healAmount);

        }
        if (thisTypeOfItems == EnumTypeOfItem.HealPotion)
        {
            HealHPEffect(healAmount);
        }

    }

    //  --- USE ITEM FOR BULDING HERE SWITCH CASE ----
    private void UseItem()
    {
        itemInfoUI.SetActive(false);

        InventorySystem.Instance.isInventoryOpen = false;
        InventorySystem.Instance.inventoryScreenUI.SetActive(false);

        ShopManager.Instance.isShopOpen = false;
        ShopManager.Instance.ShopsCatelogyUI.SetActive(false);

        CraftingManager.Instance.IsCraftOpen = false;
        CraftingManager.Instance.craftingSceneUI.SetActive(false);
        CraftingManager.Instance.toolsCatelogyUI.SetActive(false);
        CraftingManager.Instance.ConstructionCatelogyUI.SetActive(false);
        CraftingManager.Instance.ItemCatelogyUI.SetActive(false);
        CraftingManager.Instance.descriptionRequireUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InteractionManager.Instance.EnableSelection();
        InteractionManager.Instance.enabled = true;

        switch (gameObject.name)
        {
            case "WoodFoundation(Clone)":
                ConstructionManager.Instance.itemTobeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("FoundationModel");
                break;
            case "WoodFoundation":
                ConstructionManager.Instance.itemTobeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("FoundationModel");
                break;

            case "WoodWall(Clone)":
                ConstructionManager.Instance.itemTobeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("WoodWallModel");
                break;
            case "WoodWall":
                ConstructionManager.Instance.itemTobeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("WoodWallModel");
                break;

            case "WoodDoor(Clone)":
                ConstructionManager.Instance.itemTobeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("WoodDoorModel");
                break;

            case "WoodDoor":
                ConstructionManager.Instance.itemTobeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("WoodDoorModel");
                break;

            case "WoodCell":
                ConstructionManager.Instance.itemTobeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("WoodCellModel");
                break;
            case "WoodCell(Clone)":
                ConstructionManager.Instance.itemTobeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("WoodCellModel");
                break;

            case "HealPlace(Clone)":
                ConstructionManager.Instance.itemTobeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("HealPlaceModel");
                break;
            case "HealPlace":
                ConstructionManager.Instance.itemTobeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("HealPlaceModel");
                break;

            case "StoreBox(Clone)":
                PlacementSystem.Instance.inventoryItemToDestory = gameObject;
                PlacementSystem.Instance.ActivatePlacementMode("StoreBoxModel");
                break;
            case "StoreBox":
                PlacementSystem.Instance.inventoryItemToDestory = gameObject;
                PlacementSystem.Instance.ActivatePlacementMode("StoreBoxModel");              
                break;
            default:
                break;
        }


    }
    // -------------- *** STATIC CONSUME MENTHOD O DAY *** ---------------
    private static void HealEnegyEffect(int amount)
    {
        PlayerStatusManager.Instance.IncreaseEnegy(amount);


    }
    private static void HealHPEffect(int amount)
    {
        PlayerStatusManager.Instance.IncreaseHP(amount);

    }
    private static void IncreaseMoneyEffect(int amount)
    {
        PlayerStatusManager.Instance.IncreaseMoney(amount);
    }

    private static void AddTaticalBoom()
    {
        Instantiate(Resources.Load<GameObject>("GrenadeBoom_model"), ShopManager.Instance.itemPrefabSpawnPoint.position, Quaternion.identity);
    }

    private static void AddLethalBoom()
    {
        Instantiate(Resources.Load<GameObject>("SmokeBoom_model"), ShopManager.Instance.itemPrefabSpawnPoint.position, Quaternion.identity);
    }



}