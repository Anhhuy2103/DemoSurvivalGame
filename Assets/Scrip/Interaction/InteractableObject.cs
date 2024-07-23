using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;
    public bool interactable;
    public string ItemName;
    public float timeDecay = 100f;
    public float timeDelay = 0f;
    public float maxtimeDecay = 100f;

    public enum typeOfDecayItem
    {
        canDecay,
        noDecay
    }
    public typeOfDecayItem itemDecayMode;
    private void Start()
    {
        timeDecay = maxtimeDecay;
    }
  

    private void inIt()
    {
        if (interactable)
        {
            GlobalReferences.Instance.MaxtickTime = maxtimeDecay;
            GlobalReferences.Instance.tickTime = timeDecay;
        }

    }
    public string GetItemName()
    {
        return ItemName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        InputDirection();

        inIt();
    }

    private void InputDirection()
    {
        if (Input.GetKeyDown(KeyCode.F) && InteractionManager.Instance.playerInRange == true
            && InteractionManager.Instance.onTarget && InteractionManager.Instance.hoveredSelectedObject == gameObject)
        {
            //if inventory not full then: 

            if (!InventorySystem.Instance.CheckIfFull())
            {
                InventorySystem.Instance.AddToInventory(ItemName);
                InventorySystem.Instance.itemsPickedup.Add(gameObject.name);
                InteractionManager.Instance.timeDecaytext.gameObject.SetActive(false);
                Destroy(gameObject);
            }

            else
            {
                InteractionManager.Instance.timeDecaytext.gameObject.SetActive(true);
                InventorySystem.Instance.PlayInventoryFullEffect();
                Debug.Log("Inventory is Full");
            }
        }
    }

    private void delay()
    {
        this.timeDecay -= Time.deltaTime;
        GlobalReferences.Instance.tickTime = this.timeDecay;
        if (this.timeDecay <= this.timeDelay)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") && itemDecayMode== typeOfDecayItem.canDecay)
        {
            delay();
        }
    }
}
