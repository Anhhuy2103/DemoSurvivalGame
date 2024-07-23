using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerDataSo_1;
    public static MainMenuSystem Instance { get; set; }
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

    public void NewGame()
    {     
        SetPlayerDataNewGame();
    }
   

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
Application.Quit();

#endif

    }

    //------ Setting Player Data As New game -------

    public void SetPlayerDataNewGame()
    {
        playerDataSo_1.maxHP = 100;
        playerDataSo_1.maxEnegy = 100;
        playerDataSo_1.maxEXP = 10;

        playerDataSo_1.CurrentHP = 100;
        playerDataSo_1.CurrentEXP = 100;
        playerDataSo_1.CurrentEnegy = 100;
        playerDataSo_1.CurrentCoin = 100;

        playerDataSo_1.playerLevel = 0;
    }

    //------ Setting Sound Effects Button or AnyThing at MainMenu -------
    public void Play_PointerSFX()
    {
        SoundMainMenu.Instance.Play_PointerSFX();
    }
    public void Play_PressSFX()
    {
        SoundMainMenu.Instance.Play_PressSFX();
    }
}
