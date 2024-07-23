using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadSlot : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected TextMeshProUGUI buttonText;
    [SerializeField] protected int _slotNumber;
    public int SlotNumber => _slotNumber;
    private void Start()
    {

        button.onClick.AddListener(() =>
        {
            if (SaveManager.Instance.isSlotEmpty(_slotNumber) == false)
            {
                SaveManager.Instance.StartLoadedGame(_slotNumber);
                SaveManager.Instance.DeselectButton();
            }

            else
            {

            }
        });
    }
    private void Update()
    {
        if (SaveManager.Instance.isSlotEmpty(_slotNumber))
        {
            buttonText.text = "";
        }
        else
        {
            buttonText.text = PlayerPrefs.GetString("Slot" + _slotNumber + " Description");
        }

    }

}
