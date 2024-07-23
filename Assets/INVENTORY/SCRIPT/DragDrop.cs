using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    //[SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;


    private void Awake()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }


    public void OnBeginDrag(PointerEventData eventData)
    {

        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        //So the ray cast will ignore the item itself.
        canvasGroup.blocksRaycasts = false;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.root);
        itemBeingDragged = gameObject;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //So the item will move with our mouse (at same speed)  and so it will be consistant if the canvas has a different scale (other then 1);
        rectTransform.anchoredPosition += eventData.delta; /*/ canvas.scaleFactor;*/

    }



    public void OnEndDrag(PointerEventData eventData)
    {
        var termpItemReference = itemBeingDragged;
        itemBeingDragged = null;

        if (transform.parent == startParent || transform.parent == transform.root)
        {
            // Drop item into the World
            // hide the icon  of the item  at this  point
            termpItemReference.SetActive(false);

           AlertDialogManager dialogManager = FindObjectOfType<AlertDialogManager>();
            dialogManager.ShowDialog("Sure to Drop this Item?", (response) =>
            {
                if (response)
                {
                    DropItemIntoTheWorld(termpItemReference);
                }
                else
                {
                    transform.position = startPosition;
                    transform.SetParent(startParent);

                    termpItemReference.SetActive(true);
                }
            });
        }

        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    private void DropItemIntoTheWorld(GameObject termpItemReference)
    {
        // get clean name
        string cleanname = termpItemReference.name.Split(new string[] {"(Clone)"}, StringSplitOptions.None)[0];

        // Instantiate item
        GameObject item = Instantiate(Resources.Load<GameObject>(cleanname + "_model"));

        item.transform.position = Vector3.zero;
        var dropSpawnItem = PlayerStatusManager.Instance.playerBody.transform.Find("--SpawnGunPoint--").transform.position;
        item.transform.localPosition = new Vector3(dropSpawnItem.x, dropSpawnItem.y, dropSpawnItem.z);

        //set instantiate item  to be the child  of  [item] object

        var itemObject = FindObjectOfType<EnvironmentManager>().gameObject.transform.Find("[Items]");
        item.transform.SetParent(itemObject.transform);
        DestroyImmediate(termpItemReference.gameObject);
        InventorySystem.Instance.ReCalculateList();
        CraftingManager.Instance.RefreshNeededItems();
        SoundManager.Instance.Play_PopUpItemSound();
        QuestManager.Instance.RefreshTrackerQuestList();
    }
}