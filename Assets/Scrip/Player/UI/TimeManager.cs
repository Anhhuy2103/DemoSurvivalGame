using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get;set; }
    public Text dayText;
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

    public int dayIngame = 1;
    private void Start()
    {

    }
    public void TriggerNextday()
    {
        dayIngame += 1;
        dayText.text = $"DAY : {dayIngame}";
    }
}
