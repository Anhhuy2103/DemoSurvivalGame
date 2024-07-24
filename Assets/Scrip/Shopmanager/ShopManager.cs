using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [Header("Item Info")]
    public GameObject ShopsCatelogyUI;
    public GameObject descriptionRequireUI;
    public Button BuyButton;
    public bool isShopOpen;
    [ReadOnly] public int PlayerMoney;
    public Transform itemPrefabSpawnPoint;

    public static ShopManager Instance { get; set; }
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

    #region || ----------------------- ITEM COST -------------------------||
    //All ----------- BLUEPRINT ITEM ------ ( Ban ve ) --------- 
    // == 1: TOOLS
    [NonSerialized] public ShopItemCost CheapAxeCost = new ShopItemCost("AXECheap", 50); // Luu y: so 2 la total yeu cau nha.
    [NonSerialized] public ShopItemCost ThorAxeCost = new ShopItemCost("AXEThor", 100); // Luu y: so 2 la total yeu cau nha.
    [NonSerialized] public ShopItemCost RedPotionCost = new ShopItemCost("HP Bottle", 100); // Luu y: so 2 la total yeu cau nha.
    [NonSerialized] public ShopItemCost GreenPotionCost = new ShopItemCost("Enegy Bottle", 250); // Luu y: so 2 la total yeu cau nha.
    [NonSerialized] public ShopItemCost RiffleAmmoCost = new ShopItemCost("RiffleAmmoBox_model", 250); // Luu y: so 2 la total yeu cau nha.



    #endregion


    private void Start()
    {
        ShopsCatelogyUI.SetActive(false);
        BuyButton.gameObject.SetActive(false);
        PlayerMoney = PlayerStatusManager.Instance.playerdataSo.CurrentCoin;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !ConstructionManager.Instance.inConstructionMode)
        {
            ToggleShopOpen();
        }
        PlayerMoney = PlayerStatusManager.Instance.playerdataSo.CurrentCoin;
    }




    public void ToggleShopOpen()
    {
        ShopsCatelogyUI.SetActive(!ShopsCatelogyUI.activeSelf);

        if (ShopsCatelogyUI.activeInHierarchy)
        {
            CraftingManager.Instance.toolsCatelogyUI.SetActive(false);
            CraftingManager.Instance.ItemCatelogyUI.SetActive(false);
            CraftingManager.Instance.ConstructionCatelogyUI.SetActive(false);
            CraftingManager.Instance.CraftButton.gameObject.SetActive(false);

            descriptionRequireUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isShopOpen = true;
            InteractionManager.Instance.DisableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = false;
        }
        else
        {
            if (!InventorySystem.Instance.isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            ShopsCatelogyUI.SetActive(false);
            descriptionRequireUI.SetActive(false);
            isShopOpen = false;
            InteractionManager.Instance.EnableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
        }
    }

    public void ClickToCloseShopUI()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ShopsCatelogyUI.SetActive(false);
        descriptionRequireUI.SetActive(false);
        isShopOpen = false;
        InteractionManager.Instance.EnableSelection();
        InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
    }
    public void ClickToOpenShopUI()
    {

        CraftingManager.Instance.toolsCatelogyUI.SetActive(false);
        CraftingManager.Instance.ItemCatelogyUI.SetActive(false);
        CraftingManager.Instance.ConstructionCatelogyUI.SetActive(false);
        CraftingManager.Instance.CraftButton.gameObject.SetActive(false);
        ShopsCatelogyUI.SetActive(true);
        descriptionRequireUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        isShopOpen = true;
        InteractionManager.Instance.DisableSelection();
        InteractionManager.Instance.GetComponent<InteractionManager>().enabled = false;
    }
    void MenuBuyAnyItem(ShopItemCost itemCost)   // can be menu Shop for any item
    {
        // add to invenyory  - The SHOP system is the same method
        if (isShopOpen)
        {
            CraftingManager.Instance.CraftButton.gameObject.SetActive(false);
        }
        InventorySystem.Instance.AddToInventory(itemCost.ItemName);
        PlayerStatusManager.Instance.DecreaseMoney(itemCost.Cost);
    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
        // check nguyen lieu can.
    }

    void SpawnItemPrefab(ShopItemCost itemCost)   // can be menu Shop for any item
    {
        Instantiate(Resources.Load<GameObject>(itemCost.ItemName), itemPrefabSpawnPoint.position, Quaternion.identity);
        PlayerStatusManager.Instance.DecreaseMoney(itemCost.Cost);
    }
    //-----***** BUTTON CLICK CATALOGE *****-----

    // -------- TYPE: AXE ------
    // --== 1: Cheap Axe
    public void AXEClick()
    {
        BuyButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(Cheap_Axe_CRT());
        BuyButton.onClick.AddListener(delegate { MenuBuyAnyItem(CheapAxeCost); StartCoroutine(Cheap_Axe_CRT()); });
    }

    private IEnumerator Cheap_Axe_CRT()
    {
        yield return 0;
        CraftingManager.Instance.itemName_text.text = "Cheap Axe";
        CraftingManager.Instance.ResourceNeed_1.text = $"50 coins/[{PlayerMoney}]";
        CraftingManager.Instance.ResourceNeed_2.text = "";
        if (PlayerMoney >= 50)
        {
            BuyButton.gameObject.SetActive(true);
        }
        else
        {
            BuyButton.gameObject.SetActive(false);
        }
    }

    // --== : Thor Axe
    public void ThorAXEClick()
    {
        BuyButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(Thor_Cheap_Axe_CRT());
        BuyButton.onClick.AddListener(delegate { MenuBuyAnyItem(ThorAxeCost); StartCoroutine(Thor_Cheap_Axe_CRT()); });
    }

    private IEnumerator Thor_Cheap_Axe_CRT()
    {
        yield return 0;
        CraftingManager.Instance.itemName_text.text = "Lighting Axe";
        CraftingManager.Instance.ResourceNeed_1.text = $"100 coins/[{PlayerMoney}]";
        CraftingManager.Instance.ResourceNeed_2.text = "";
        if (PlayerMoney >= 100)
        {
            BuyButton.gameObject.SetActive(true);
        }
        else
        {
            BuyButton.gameObject.SetActive(false);
        }
    }

    // --== 3 : Red Potion
    public void RedPotionClick()
    {
        BuyButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(RedPotion_CRT());
        BuyButton.onClick.AddListener(delegate { MenuBuyAnyItem(RedPotionCost); StartCoroutine(RedPotion_CRT()); });
    }

    private IEnumerator RedPotion_CRT()
    {
        yield return 0;
        CraftingManager.Instance.itemName_text.text = "RedPotion";
        CraftingManager.Instance.ResourceNeed_1.text = $"100 coins/[{PlayerMoney}]";
        CraftingManager.Instance.ResourceNeed_2.text = "";
        if (PlayerMoney >= 100)
        {
            BuyButton.gameObject.SetActive(true);
        }
        else
        {
            BuyButton.gameObject.SetActive(false);
        }
    }

    // --== 4 : Grreen Potion
    public void GreenPotionClick()
    {
        BuyButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(GreenPotion_CRT());
        BuyButton.onClick.AddListener(delegate { MenuBuyAnyItem(GreenPotionCost); StartCoroutine(GreenPotion_CRT()); });
    }

    private IEnumerator GreenPotion_CRT()
    {
        yield return 0;
        CraftingManager.Instance.itemName_text.text = "RedPotion";
        CraftingManager.Instance.ResourceNeed_1.text = $"250 coins/[{PlayerMoney}]";
        CraftingManager.Instance.ResourceNeed_2.text = "";
        if (PlayerMoney >= 250)
        {
            BuyButton.gameObject.SetActive(true);
        }
        else
        {
            BuyButton.gameObject.SetActive(false);
        }
    }




    #region || -------------------------------- AMMO AND GRENADE ONLY -------------
    // --== 4 : Grreen Potion
    public void RiffleAmmoClick()
    {
        BuyButton.onClick.RemoveAllListeners();
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(RiffleAmmo_CRT());
        BuyButton.onClick.AddListener(delegate { SpawnItemPrefab(RiffleAmmoCost); StartCoroutine(RiffleAmmo_CRT()); });
    }

    private IEnumerator RiffleAmmo_CRT()
    {
        yield return 0;
        CraftingManager.Instance.itemName_text.text = "Riffle Ammo / 100ps";
        CraftingManager.Instance.ResourceNeed_1.text = $"250 coins/[{PlayerMoney}]";
        CraftingManager.Instance.ResourceNeed_2.text = "";
        if (PlayerMoney >= 250)
        {
            BuyButton.gameObject.SetActive(true);
        }
        else
        {
            BuyButton.gameObject.SetActive(false);
        }
    }
    #endregion
}
