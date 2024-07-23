using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] GameObject ShopTriggerSceneUI;


    private void Start()
    {
        ShopTriggerSceneUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.gameObject.CompareTag("Player") && !InventorySystem.Instance.isInventoryOpen)
        {
            ShopTriggerSceneUI.SetActive(true);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other == null) return;
        if (other.gameObject.CompareTag("Player") && !InventorySystem.Instance.isInventoryOpen)
        {
            ShopTriggerSceneUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !ConstructionManager.Instance.inConstructionMode)
            {
                ShopManager.Instance.ToggleShopOpen();
                ShopManager.Instance.isShopOpen = true;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other == null) return;
        if (other.gameObject.CompareTag("Player") && !InventorySystem.Instance.isInventoryOpen)
        {
            ShopTriggerSceneUI.SetActive(false);
            ShopManager.Instance.ShopsCatelogyUI.SetActive(false);


            if (!InventorySystem.Instance.isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            CraftingManager.Instance.craftingSceneUI.SetActive(false);
            CraftingManager.Instance.ItemCatelogyUI.SetActive(false);

            CraftingManager.Instance.ConstructionCatelogyUI.SetActive(false);
            CraftingManager.Instance.toolsCatelogyUI.SetActive(false);
            ShopManager.Instance.descriptionRequireUI.SetActive(false);         
            InteractionManager.Instance.EnableSelection();
            InteractionManager.Instance.GetComponent<InteractionManager>().enabled = true;
        }

    }
}
