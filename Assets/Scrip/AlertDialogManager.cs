using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertDialogManager : MonoBehaviour
{
    public static AlertDialogManager Instance { get; set; }

    public GameObject dialogBox;
    public TextMeshProUGUI messageText;
    public Button okButton;
    public Button cancelButton;


    private System.Action<bool> responseCallback;
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
         dialogBox.SetActive(false);

        okButton.onClick.AddListener(() => HandleResponse(true));
        cancelButton.onClick.AddListener(() => HandleResponse(false));
    }

    public void ShowDialog(string message,System.Action<bool> callback)
    {
        responseCallback = callback;
        messageText.text = message;
        dialogBox.SetActive(true);
    }

    private void HandleResponse(bool responce)
    {
        dialogBox.SetActive(false);
        responseCallback?.Invoke(responce);
    }
}
