using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ToolEnum;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance { get; set; }
    [Header("Crafting_UI")]
    public GameObject craftingSceneUI, descriptionRequireUI;
    public GameObject ItemCatelogyUI;
    public GameObject ConstructionCatelogyUI;
    public GameObject toolsCatelogyUI;



    [NonSerialized] public List<string> InventoryItemList = new List<string>();
    [NonSerialized] public bool IsCraftOpen;
    [Header("Text")]
    // ResourceNeed Text
    public TextMeshProUGUI ResourceNeed_1, ResourceNeed_2;

    public TextMeshProUGUI itemName_text;

    //------ Craft Type Button -------
    [Header("Button On Crafting Scene")]
    // Craft Button
    public Button CraftButton;
    // Category Button
    [SerializeField] private Button toolsBTN;
    [SerializeField] private Button ItemBTN;
    [SerializeField] private Button ConstructionBTN;



    // --- Resource Count ---
    private int stoneCount;
    private int woodlogCount;
    private int woodPlankCount;

    private int grassCount;
    private int greenFruitCount;
    private int redFruitCount;
    private int goldCount;



    //All ----------- BLUEPRINT ITEM ------ ( Ban ve ) --------- 
    // == 1: TOOLS
    [NonSerialized] public Blueprint CheapAxeBLP = new Blueprint("AXECheap", 2, "Stone", 3, "WoodLog", 1); // Luu y: so 2 la total yeu cau nha.
    [NonSerialized] public Blueprint CheapPickAxe = new Blueprint("CheapPickAxe", 2, "Stone", 3, "WoodLog", 1);
    [NonSerialized] public Blueprint AXEThorBLP = new Blueprint("AXEThor", 2, "Stone", 5, "WoodLog", 1);
    // == 2: Item
    [NonSerialized] public Blueprint RedPotionBLP = new Blueprint("HP Bottle", 2, "Grass", 1, "RedFruit", 1);
    [NonSerialized] public Blueprint GreenPotionBLP = new Blueprint("Enegy Bottle", 2, "Grass", 1, "GreenFruit", 1);
    [NonSerialized] public Blueprint WoodPlank = new Blueprint("WoodPlank", 1, "WoodLog", 1, "", 0);
    [NonSerialized] public Blueprint StoreBox = new Blueprint("StoreBox", 1, "WoodPlank", 1, "", 0);

    // == 3: Construction
    [NonSerialized] public Blueprint WoodFoundation = new Blueprint("WoodFoundation", 2, "WoodPlank", 1, "Grass", 1);
    [NonSerialized] public Blueprint WoodWall = new Blueprint("WoodWall", 2, "WoodPlank", 1, "Grass", 1);




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
        IsCraftOpen = false;
        craftingSceneUI.SetActive(false);
        toolsCatelogyUI.SetActive(false);
        ItemCatelogyUI.SetActive(false);

        ConstructionCatelogyUI.SetActive(false);
        descriptionRequireUI.SetActive(false);

        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });
        ItemBTN.onClick.AddListener(delegate { OpenItemCategory(); });
        ConstructionBTN.onClick.AddListener(delegate { OpenConstructionCategory(); });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && !ConstructionManager.Instance.inConstructionMode)
        {
            ToggleCraftingScene();
        }

    }
    // ---------- CRAFTING ITEM BACKEND MENTHOD ----------


    void OpenToolsCategory()
    {
        craftingSceneUI.SetActive(false);
        toolsCatelogyUI.SetActive(true);
        IsCraftOpen = true;
    }
    void OpenItemCategory()
    {
        craftingSceneUI.SetActive(false);
        ItemCatelogyUI.SetActive(true);
        IsCraftOpen = true;
    }
    void OpenConstructionCategory()
    {
        craftingSceneUI.SetActive(false);
        ConstructionCatelogyUI.SetActive(true);
        IsCraftOpen = true;
    }

    private void ToggleCraftingScene()
    {
        craftingSceneUI.SetActive(!craftingSceneUI.activeSelf);

        if (craftingSceneUI.activeInHierarchy)
        {
            toolsCatelogyUI.SetActive(false);
            ItemCatelogyUI.SetActive(false);
            ConstructionCatelogyUI.SetActive(false);

            descriptionRequireUI.SetActive(false);
            ShopManager.Instance.ShopsCatelogyUI.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            IsCraftOpen = true;
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
            craftingSceneUI.SetActive(false);
            ItemCatelogyUI.SetActive(false);
            ShopManager.Instance.ShopsCatelogyUI.gameObject.SetActive(false);
            ConstructionCatelogyUI.SetActive(false);
            toolsCatelogyUI.SetActive(false);
            descriptionRequireUI.SetActive(false);
            IsCraftOpen = false;
            InteractionManager.Instance.EnableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
        }
    }

    public void OnClickToBackToCraftingScene()
    {
        craftingSceneUI.SetActive(true);
        toolsCatelogyUI.SetActive(false);
        ConstructionCatelogyUI.SetActive(false);
        descriptionRequireUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    public void OnClicktoCloseRequirementPanel()
    {
        descriptionRequireUI.gameObject.SetActive(false);
        CraftButton.gameObject.SetActive(false);

    }
    //---- MenuCraft Item ---
    void MenuCraftAnyItem(Blueprint blueprintToCraft)   // can be menu Shop for any item
    {
        if (IsCraftOpen)
        {
            ShopManager.Instance.BuyButton.gameObject.SetActive(false);

        }
        // add to invenyory  - The SHOP system is the same method
        InventorySystem.Instance.AddToInventory(blueprintToCraft.BPName);


        // remove item from inventory -  The Shop System is the same ( minus coin )
        if (blueprintToCraft.totalOfRequirements == 1) // check bao nhieu yeu cau o day. dung if va else if
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.BpReq1, blueprintToCraft.BPReqamount1);
        }
        else if (blueprintToCraft.totalOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.BpReq1, blueprintToCraft.BPReqamount1);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.BpReq2, blueprintToCraft.BPReqamount2);
        }
        // neu muon them so luong yeu cau, => Blueprint => add them BpRed3 => lam construc . done.


        StartCoroutine(calculate());
    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
        QuestManager.Instance.RefreshTrackerQuestList();
        // check nguyen lieu can.
        RefreshNeededItems();
    }

    // ----------------- CHECK COUNT CRAFT O DAY NE ------------------
    public void RefreshNeededItems()
    {
        int Stone_count = 0;
        stoneCount = Stone_count;
        int WoodLog_count = 0;
        woodlogCount = WoodLog_count;
        int WoodPlank_count = 0;
        woodPlankCount = WoodPlank_count;
        int Grass_count = 0;
        grassCount = Grass_count;
        int Gold_count = 0;
        goldCount = Gold_count;
        int RedFruit_count = 0;
        redFruitCount = RedFruit_count;
        int GreenFruit_count = 0;
        greenFruitCount = GreenFruit_count;

        InventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in InventoryItemList) // check name cua item trong inventory
        {

            switch (itemName)
            {
                case "Stone":
                    Stone_count += 1;
                    stoneCount = Stone_count;
                    break;

                case "WoodLog":
                    WoodLog_count += 1;
                    woodlogCount = WoodLog_count;
                    break;

                case "WoodPlank":
                    WoodPlank_count += 1;
                    woodPlankCount = WoodPlank_count;
                    break;

                case "Grass":
                    Grass_count += 1;
                    grassCount = Grass_count;
                    break;

                case "Gold":
                    Gold_count += 1;
                    goldCount = Gold_count;
                    break;

                case "RedFruit":
                    RedFruit_count += 1;
                    redFruitCount = RedFruit_count;
                    break;

                case "GreenFruit":
                    GreenFruit_count += 1;
                    greenFruitCount = GreenFruit_count;
                    break;
            }
        }
    }
    //-----***** BUTTON CLICK CATALOGE *****-----

    // -------- TYPE: AXE ------
    // --== 1: Cheap Axe
    public void AXEClick()
    {
        CraftButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(Cheap_Axe_CRT());
        CraftButton.onClick.AddListener(delegate { MenuCraftAnyItem(CheapAxeBLP); StartCoroutine(Cheap_Axe_CRT()); });
    }

    private IEnumerator Cheap_Axe_CRT()
    {
        yield return 0;
        itemName_text.text = "Cheap Axe";
        ResourceNeed_1.text = $"3 Stone/[{stoneCount}]";
        ResourceNeed_2.text = $"1 WoodLog/[{woodlogCount}]";
        if (stoneCount >= 3 && woodlogCount >= 1)
        {
            CraftButton.gameObject.SetActive(true);
        }
        else
        {
            CraftButton.gameObject.SetActive(false);
        }
    }
    // --== 1: Cheap PickAxe
    public void PickAXEClick()
    {
        CraftButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(Pick_Axe_CRT());
        CraftButton.onClick.AddListener(delegate { MenuCraftAnyItem(CheapPickAxe); StartCoroutine(Pick_Axe_CRT()); });
    }

    private IEnumerator Pick_Axe_CRT()
    {
        yield return 0;
        itemName_text.text = "Cheap PickAxe";
        ResourceNeed_1.text = $"3 Stone/[{stoneCount}]";
        ResourceNeed_2.text = $"1 WoodLog/[{woodlogCount}]";
        if (stoneCount >= 3 && woodlogCount >= 1)
        {
            CraftButton.gameObject.SetActive(true);
        }
        else
        {
            CraftButton.gameObject.SetActive(false);
        }
    }

    //--== 2: Thor Axe
    public void AXEThorClick()
    {

        CraftButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(AXE_Thor_CRT());
        CraftButton.onClick.AddListener(delegate { MenuCraftAnyItem(AXEThorBLP); StartCoroutine(AXE_Thor_CRT()); });
    }
    private IEnumerator AXE_Thor_CRT()
    {
        yield return 0;
        itemName_text.text = "AXE Thor";
        ResourceNeed_1.text = $"5 Stone/[{stoneCount}]";
        ResourceNeed_2.text = $"1 WoodLog/[{woodlogCount}]";
        if (stoneCount >= 5 && woodlogCount >= 1)
        {
            CraftButton.gameObject.SetActive(true);
        }
        else
        {
            CraftButton.gameObject.SetActive(false);
        }
    }

    //----------Item Catelogy -----------
    // 1: RedPotion =======

    public void RedPotionClick()
    {

        CraftButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(RedPotion_CRT());
        CraftButton.onClick.AddListener(delegate { MenuCraftAnyItem(RedPotionBLP); StartCoroutine(RedPotion_CRT()); });
    }
    private IEnumerator RedPotion_CRT()
    {
        yield return 0;
        itemName_text.text = "Red Potion";
        ResourceNeed_1.text = $"1 Grass/[{grassCount}]";
        ResourceNeed_2.text = $"1 Red Fruit/[{redFruitCount}]";
        if (grassCount >= 1 && redFruitCount >= 1)
        {
            CraftButton.gameObject.SetActive(true);
        }
        else
        {
            CraftButton.gameObject.SetActive(false);
        }
    }
    // 1: Green Potion  =======

    public void GreenPotionClick()
    {

        CraftButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(GreenPotion_CRT());
        CraftButton.onClick.AddListener(delegate { MenuCraftAnyItem(GreenPotionBLP); StartCoroutine(GreenPotion_CRT()); });
    }
    private IEnumerator GreenPotion_CRT()
    {
        yield return 0;
        itemName_text.text = "Green Potion";
        ResourceNeed_1.text = $"1 Grass/[{grassCount}]";
        ResourceNeed_2.text = $"1 Green Fruit/[{greenFruitCount}]";
        if (grassCount >= 1 && greenFruitCount >= 1)
        {
            CraftButton.gameObject.SetActive(true);
        }
        else
        {
            CraftButton.gameObject.SetActive(false);
        }
    }

    // 1: WoodBoard  =======

    public void WoodBoardClick()
    {

        CraftButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(WoodBoard_CRT());
        CraftButton.onClick.AddListener(delegate { MenuCraftAnyItem(WoodPlank); StartCoroutine(WoodBoard_CRT()); });
    }
    private IEnumerator WoodBoard_CRT()
    {
        yield return 0;
        itemName_text.text = "WoodPlank";
        ResourceNeed_1.text = $"1 WoodLog/[{woodlogCount}]";
        ResourceNeed_2.text = "";
        if (woodlogCount >= 1)
        {
            CraftButton.gameObject.SetActive(true);
        }
        else
        {
            CraftButton.gameObject.SetActive(false);
        }
    }

    // 1: Wood Foundation =======

    public void WoodFoundationClick()
    {

        CraftButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(WoodFoundation_CRT());
        CraftButton.onClick.AddListener(delegate { MenuCraftAnyItem(WoodFoundation); StartCoroutine(WoodFoundation_CRT()); });
    }
    private IEnumerator WoodFoundation_CRT()
    {
        yield return 0;
        itemName_text.text = "WoodFoundation";
        ResourceNeed_1.text = $"1 WoodPlank/[{woodPlankCount}]";
        ResourceNeed_2.text = $"1 Grass/[{grassCount}]";
        if (woodPlankCount >= 1 && grassCount >= 1)
        {
            CraftButton.gameObject.SetActive(true);
        }
        else
        {
            CraftButton.gameObject.SetActive(false);
        }
    }

    // 1: Wood Wall =======

    public void WoodWallClick()
    {

        CraftButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(WoodWall_CRT());
        CraftButton.onClick.AddListener(delegate { MenuCraftAnyItem(WoodWall); StartCoroutine(WoodWall_CRT()); });
    }
    private IEnumerator WoodWall_CRT()
    {
        yield return 0;
        itemName_text.text = "WoodWall";
        ResourceNeed_1.text = $"1 WoodPlank/[{woodPlankCount}]";
        ResourceNeed_2.text = $"1 Grass/[{grassCount}]";
        if (woodPlankCount >= 1 && grassCount >= 1)
        {
            CraftButton.gameObject.SetActive(true);
        }
        else
        {
            CraftButton.gameObject.SetActive(false);
        }
    }

    // 1: WoodBoard  =======

    public void StorageBoxClick()
    {

        CraftButton.onClick.RemoveAllListeners();
        StartCoroutine(calculate());
        descriptionRequireUI.gameObject.SetActive(true);
        StartCoroutine(StorageBox_CRT());
        CraftButton.onClick.AddListener(delegate { MenuCraftAnyItem(StoreBox); StartCoroutine(StorageBox_CRT()); });
    }
    private IEnumerator StorageBox_CRT()
    {
        yield return 0;
        itemName_text.text = "Small Storage Box";
        ResourceNeed_1.text = $"1 WoodPlank/[{woodPlankCount}]";
        ResourceNeed_2.text = "";
        if (woodPlankCount >= 1)
        {
            CraftButton.gameObject.SetActive(true);
        }
        else
        {
            CraftButton.gameObject.SetActive(false);
        }
    }
    //-------------------- SHOP BUY BUTTON -----------------
}
