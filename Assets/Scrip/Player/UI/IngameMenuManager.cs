using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject LoadGameMenu;
    [SerializeField] private GameObject SettingGameMenu;
    [SerializeField] private GameObject BigMap;
    [SerializeField] private GameObject yesNoPanel;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private GameObject PlayerStatusPanel;
    [SerializeField] private GameObject middleDot;
    [SerializeField] private GameObject tutorialMenu;
    public bool isActiveMenuPanel;
    public bool isActiveMapPanel;


    public static IngameMenuManager Instance { get; set; }

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
        Init();
    }

    private void Init()
    {
        menuUI.SetActive(false);
        BigMap.SetActive(false);
        yesButton.onClick.AddListener(Quit_yesButtonn);
        noButton.onClick.AddListener(Quit_NoButtonn);
    }

    private void Update()
    {
        InPutDirection();
    }

    private void InPutDirection()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !DialogManager.Instance.isDiablogUIActive
        && !InventorySystem.Instance.isInventoryOpen
        && !CraftingManager.Instance.IsCraftOpen
        && !ConstructionManager.Instance.inConstructionMode
        && !ShopManager.Instance.isShopOpen)
        {
            toggleMenu();
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            tutorialMenu.gameObject.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.M)
           && !DialogManager.Instance.isDiablogUIActive
        && !InventorySystem.Instance.isInventoryOpen
        && !CraftingManager.Instance.IsCraftOpen
        && !ConstructionManager.Instance.inConstructionMode
        && !ShopManager.Instance.isShopOpen)
        {
            toggleMap();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleStatusPanelpen();
        }
    }

    private void SetCloseInventory()
    {
        InventorySystem.Instance.inventoryScreenUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InventorySystem.Instance.isInventoryOpen = false;
    }
    private void toggleMenu()
    {
        menuUI.SetActive(!menuUI.activeSelf);

        if (menuUI.activeInHierarchy)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isActiveMenuPanel = true;

            InteractionManager.Instance.DisableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            if (!CraftingManager.Instance.IsCraftOpen && !InventorySystem.Instance.isInventoryOpen && !ShopManager.Instance.isShopOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            yesNoPanel.SetActive(false);
            LoadGameMenu.SetActive(false);
            SettingGameMenu.SetActive(false);
            isActiveMenuPanel = false;
            InteractionManager.Instance.EnableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
        }
    }
    public void ToggleStatusPanelpen()
    {
        PlayerStatusPanel.SetActive(!PlayerStatusPanel.activeSelf);

        if (PlayerStatusPanel.activeInHierarchy)
        {

            PlayerStatusPanel.gameObject.SetActive(true);


        }
        else
        {
            PlayerStatusPanel.gameObject.SetActive(false);
        }
    }
    private void toggleMap()
    {
        BigMap.SetActive(!BigMap.activeSelf);

        if (BigMap.activeInHierarchy)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isActiveMapPanel = true;
            yesNoPanel.SetActive(false);
            LoadGameMenu.SetActive(false);
            SettingGameMenu.SetActive(false);
            menuUI.SetActive(false);
            InteractionManager.Instance.DisableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = false;
        }
        else
        {
            if (!CraftingManager.Instance.IsCraftOpen && !InventorySystem.Instance.isInventoryOpen && !ShopManager.Instance.isShopOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            Time.timeScale = 1;

            isActiveMapPanel = false;
            InteractionManager.Instance.EnableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
        }
    }

    public void ExitPanel_Button()
    {
        yesNoPanel.SetActive(true);

    }
    public void Quit_yesButtonn()
    {

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void Quit_NoButtonn()
    {
        Time.timeScale = 1;
        if (!CraftingManager.Instance.IsCraftOpen && !InventorySystem.Instance.isInventoryOpen && !ShopManager.Instance.isShopOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        menuUI.SetActive(false);
        yesNoPanel.SetActive(false);
        LoadGameMenu.SetActive(false);
        SettingGameMenu.SetActive(false);
        isActiveMenuPanel = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InteractionManager.Instance.EnableSelection();
        InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
    }

    public void openTutorial()
    {
        tutorialMenu.SetActive(!tutorialMenu.activeSelf);

        if (tutorialMenu.activeInHierarchy)
        {

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            InteractionManager.Instance.DisableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = false;
        }
        else
        {
            if (!InventorySystem.Instance.isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                InteractionManager.Instance.EnableSelection();
                InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
            }
        }
    }
}

