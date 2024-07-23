using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatusManager : MonoBehaviour
{

    public static PlayerStatusManager Instance;
    [Header("PlayerEffect System")]
    [SerializeField] private float tickTime;
    [SerializeField] private float goodTime;


    [Header("PlayerStatusSystem")]
    public PlayerDataSO playerdataSo;
    [SerializeField] private GameObject HurtScreen_1;
    [SerializeField] private GameObject HurtScreen_2;
    [SerializeField] private Animator cameraAnim;
    [SerializeField] private Transform playerSpawPoint;
    public bool isDead;

    [Header("PlayerStatusUI")]
    public SliderBar player_Heathbar;
    public TextMeshProUGUI player_EXPtext;
    public SliderBar player_Engybar;
    public TextMeshProUGUI _playerGoldText;
    public GameObject gameOverUI;
    public Image AngryImage;
    public Image LevelUpImage;
    protected string PlayerName;


    [Header("Player Body")]
    public GameObject playerBody;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

    }
    private void Start()
    {

        PlayerStatusDecription();
        _playerGoldText.text = playerdataSo.CurrentCoin.ToString();
        AngryImage.gameObject.SetActive(false);
        LevelUpImage.gameObject.SetActive(false);
    }

    private void Update()
    {

        PlayerStatusDecription();
        StatusPlayerFuntion();
    }

    //---------------------------------------------------------
    public void ReadInputName(string _input)
    {
        PlayerName = _input;
    }
    private void PlayerStatusDecription()
    {
        updatePlayerStatus();

    }

    // ----------------- ItemType Funtion ----------------------
    public void IncreaseHP(int amount)
    {
        if (playerdataSo.CurrentHP < playerdataSo.maxHP)
        {
            playerdataSo.CurrentHP += amount;
        }
        if (playerdataSo.CurrentHP >= playerdataSo.maxHP) playerdataSo.CurrentHP = playerdataSo.maxHP;
        player_Heathbar.setSlider(playerdataSo.CurrentHP);
    }

    public void MinusHP(int amount)
    {
        playerdataSo.CurrentHP -= amount;
        if (playerdataSo.CurrentHP <= 0)
        {
            playerdataSo.CurrentHP = 0;
        }
        player_Heathbar.setSlider(playerdataSo.CurrentHP);
    }


    public void MinusEnegy(int amount)
    {
        playerdataSo.CurrentEnegy -= amount;
        if (playerdataSo.CurrentEnegy <= 0)
        {
            playerdataSo.CurrentEnegy = 0;
        }

        player_Engybar.setSlider(playerdataSo.CurrentEnegy);

    }

    public void IncreaseEnegy(int amount)
    {
        playerdataSo.CurrentEnegy += amount;
        if (playerdataSo.CurrentEnegy >= playerdataSo.maxEnegy)
        {
            playerdataSo.CurrentEnegy = playerdataSo.maxEnegy;
        }

        player_Engybar.setSlider(playerdataSo.CurrentEnegy);
    }

    public void takeEXPandUPLevel(int amount)
    {
        playerdataSo.CurrentEXP += amount;

        if (playerdataSo.CurrentEXP >= playerdataSo.maxEXP)
        {
            LevelUp();
            playerdataSo.isLevelUp = true;
            playerdataSo.CurrentEXP = 0;
            playerdataSo.playerLevel++;
            playerdataSo.maxEXP += 10;
            playerdataSo.maxHP += 10;
            playerdataSo.maxEnegy += 10;
            if (playerdataSo.MaxLevel == 100)
            {
                playerdataSo.MaxLevel = 100;
            }
        }
        player_EXPtext.text = playerdataSo.playerLevel.ToString();
    }

    public void IncreaseMoney(int amount)
    {
        playerdataSo.CurrentCoin += amount;
        _playerGoldText.text = playerdataSo.CurrentCoin.ToString();
    }
    public void DecreaseMoney(int amount)
    {
        playerdataSo.CurrentCoin -= amount;
        _playerGoldText.text = playerdataSo.CurrentCoin.ToString();
    }

    // ----------------- Init Funtion ----------------------
    public void updatePlayerStatus()
    {
        _playerGoldText.text = playerdataSo.CurrentCoin.ToString();

        player_EXPtext.text = playerdataSo.playerLevel.ToString();

        player_Heathbar.setmaxSlider(playerdataSo.maxHP);
        player_Heathbar.setSlider(playerdataSo.CurrentHP);
        player_Engybar.setmaxSlider(playerdataSo.maxEnegy);
        player_Engybar.setSlider(playerdataSo.CurrentEnegy);
    }
    public void coinUpdate()
    {
        _playerGoldText.text = playerdataSo.CurrentCoin.ToString();
    }
    private void StatusPlayerFuntion()
    {

        if (playerdataSo.CurrentEnegy <= 0)
        {
            tickTime -= Time.deltaTime;
            AngryImage.gameObject.SetActive(true);
            if (tickTime < 0 && isDead == false)
            {
                MinusHP(5);
                tickTime = 3.0f;
            }
        }
        else
        {

            goodTime -= Time.deltaTime;
            AngryImage.gameObject.SetActive(false);
            if (goodTime < 0 && isDead == false)
            {
                IncreaseHP(5);
                goodTime = 5.0f;
            }
        }
    }
    //---------
    public void TakeDame(int damegeAmount)
    {
        playerdataSo.CurrentHP -= damegeAmount;
        player_Heathbar.setSlider(playerdataSo.CurrentHP);

        if (playerdataSo.CurrentHP <= 0)
        {
            playerdataSo.CurrentCoin -= 100;
            SoundManager.Instance.Play_PlayerDeath();
            SoundManager.Instance.Play_PlayerDeadSoundTrack();
            isDead = true;
            OnPlayerDead();
        }
        else
        {
            SoundManager.Instance.Play_PlayerHurt();
            StartCoroutine(GetHurtScreenEffect_1());
            StartCoroutine(GetHurtScreenEffect_2());
        }
    }

    private void OnPlayerDead()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
        //Dying Animator
        cameraAnim.GetComponentInChildren<Animator>().enabled = true;

    }

    private void LevelUp()
    {

        StartCoroutine(levelupCRT());

    }
    private IEnumerator levelupCRT()
    {
        SoundManager.Instance.Play_LevelUpSound();
        LevelUpImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        LevelUpImage.gameObject.SetActive(false);
    }
    // ------------- MainMenu  -------------
    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(2f);
        gameOverUI.gameObject.SetActive(true);
        StartCoroutine(ReturntoMainMenu());
    }

    private IEnumerator ReturntoMainMenu()
    {
        yield return new WaitForSeconds(8f);
        isDead = false;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<ScreenFader>().FadeOn();
        SoundManager.Instance.Play_BackGroundMusic();
        //----- Heal status and music -----
        playerdataSo.CurrentHP = playerdataSo.maxHP;
        playerdataSo.CurrentEnegy = playerdataSo.maxEnegy;
        player_Heathbar.setmaxSlider(playerdataSo.CurrentHP);
        player_Heathbar.setmaxSlider(playerdataSo.CurrentEnegy);



        //--------- Turn Off UI --------
        gameOverUI.gameObject.SetActive(false);
        cameraAnim.GetComponentInChildren<Animator>().enabled = false;
        this.gameObject.transform.position = playerSpawPoint.position;
        this.gameObject.transform.rotation = playerSpawPoint.rotation;
    }
    // ------------- HurtScene -------------
    private IEnumerator GetHurtScreenEffect_1()
    {
        if (HurtScreen_1.activeInHierarchy == false)
        {
            HurtScreen_1.SetActive(true);
        }

        var image = HurtScreen_1.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (HurtScreen_1.activeInHierarchy)
        {
            HurtScreen_1.SetActive(false);
        }
    }
    private IEnumerator GetHurtScreenEffect_2()
    {
        if (HurtScreen_2.activeInHierarchy == false)
        {
            HurtScreen_2.SetActive(true);
        }

        var image = HurtScreen_2.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }

        if (HurtScreen_2.activeInHierarchy)
        {
            HurtScreen_2.SetActive(false);
        }
    }

    // ------------- Va Cham Funtion + Dame by enemy -------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GolemHand")  // Golem
        {
            if (isDead == false)
            {
                TakeDame(other.gameObject.GetComponent<GolemHand>().damage);
            }
        }
        // ----------------- va cham GOOD Effect -------------

        if (other.CompareTag("HealPoint"))
        {
            if (isDead == false)
            {

            }
        }
    }

    //----------------- Hoi Phuc / Tru Mau theo thoi gian --------------
    private void OnTriggerStay(Collider collision)
    {
        // 1. -- Heal Point -- 
        if (collision.gameObject.tag == "HealPoint")
        {
            tickTime -= Time.deltaTime;

            if (tickTime < 0 && isDead == false)
            {
                IncreaseEnegy(20);
                IncreaseHP(20);
                tickTime = 1.0f;
            }
        }
    }

    private void autoHeal()
    {
        tickTime -= Time.deltaTime;
        if (tickTime < 0 && playerdataSo.CurrentEnegy >= 50)
        {
            IncreaseHP(10);
            tickTime = 2.0f;
        }
    }
}
