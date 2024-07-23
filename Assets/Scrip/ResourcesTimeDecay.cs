using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesTimeDecay : MonoBehaviour
{
    [Header("Infomation")]
    [SerializeField] private Text timedecayText;
    private float ticktime;
    private float Maxticktime;

    [Header("Global")]
    public GameObject GlobalState;
    // InteractableObject
    private void Start()
    {
        timedecayText = GetComponent<Text>();
        ticktime = Maxticktime;
    }
    private void Update()
    {
        inIt();
    }

    private void inIt()
    {
        GameObject selectedItemObject = InteractionManager.Instance.hoveredSelectedObject;
        if (selectedItemObject != null)
        {
            ticktime = GlobalState.GetComponent<GlobalReferences>().tickTime;
            Maxticktime = GlobalState.GetComponent<GlobalReferences>().MaxtickTime;
        }
     
        timedecayText.text = $"Decay in [{ticktime.ToString("F0")}] s";
    }
}
