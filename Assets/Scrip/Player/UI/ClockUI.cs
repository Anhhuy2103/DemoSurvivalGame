using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI time;

    protected TimeManager clock;

    private void Start()
    {
        clock = FindObjectOfType<TimeManager>();
    }
}
