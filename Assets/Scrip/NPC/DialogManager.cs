using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour 
{ 
    public static DialogManager Instance { get; set; }
  
    public TextMeshProUGUI diablogText;

    public Button optionBTN_1;
    public Button optionBTN_2;

    public Canvas diablogUI;

    public bool isDiablogUIActive;

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

    public void OpenDiablogUI()
    {
        diablogUI.gameObject.SetActive(true);
        isDiablogUIActive = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }

    public void CloseDiablogUI()
    {
        diablogUI.gameObject.SetActive(false);
        isDiablogUIActive = false;
        InteractionManager.Instance.middleCross.gameObject.SetActive(true );
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
    }
}
