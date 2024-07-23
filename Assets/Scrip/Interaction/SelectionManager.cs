using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject interaction_Info_UI;
    Text interaction_text;

    [SerializeField] private float InteracDistance = 5f;
    [SerializeField] private GameObject player;
    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            float distanceFromPlayer = Vector3.Distance(player.transform.position, selectionTransform.transform.position);

            if (selectionTransform.GetComponent<InteractableObject>() && distanceFromPlayer < InteracDistance)
            {
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                interaction_Info_UI.SetActive(false);
            }

        }
    }
}
