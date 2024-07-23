using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected TextMeshProUGUI SaveButtonText;
    [SerializeField] protected int slotNumber;

    [SerializeField] protected GameObject alertUI;
    protected Button yesBTN;
    protected Button noBTN;
    private void Awake()
    {
        alertUI.SetActive(false);

        yesBTN = alertUI.transform.Find("YesButton").GetComponent<Button>();
        noBTN = alertUI.transform.Find("NoButton").GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (SaveManager.Instance.isSlotEmpty(slotNumber))
            {
                SaveGameConfirmed();
            }
            else
            {
                DisplayOverrideWarning();
            }
        });
    }

    private void Update()
    {
        if (SaveManager.Instance.isSlotEmpty(slotNumber))
        {
            SaveButtonText.text = "Empty";
        }
        else
        {
            SaveButtonText.text = PlayerPrefs.GetString("Slot" + slotNumber + " Description");
        }
    }
    public void DisplayOverrideWarning()
    {
        alertUI.SetActive(true);

        yesBTN.onClick.AddListener(() =>
        {
            SaveGameConfirmed();
            alertUI.SetActive(false);

        });

        noBTN.onClick.AddListener(() =>
        {
            alertUI.SetActive(false);
        });
    }

    public void SaveGameConfirmed()
    {
        SaveManager.Instance.SaveGame(slotNumber);

        DateTime dt = DateTime.Now;
        string time = dt.ToString("yyyy-MM-dd HH:mm");

        string Description = "Saved Game " + slotNumber + " | " + time;

        SaveButtonText.text = Description;

        PlayerPrefs.SetString("Slot" + slotNumber + " Description", Description);

        SaveManager.Instance.DeselectButton();
    }
}
