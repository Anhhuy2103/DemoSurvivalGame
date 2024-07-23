using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveManager;

public class SoundMainMenu : MonoBehaviour
{

    public static SoundMainMenu Instance { get; set; }

    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioSource m_SFXSource;
    [SerializeField] private AudioClip MusicBackGround;
    [SerializeField] private AudioClip m_pointerClip;
    [SerializeField] private AudioClip m_ClickClip;
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
        InitVolumeValueSetting();
    }

    private void Update()
    {
        InitVolumeValueSetting();
    }
    private void InitVolumeValueSetting()
    {      
            m_AudioSource.volume = SettingManager.Instance.musicSlider.value;
            m_SFXSource.volume = SettingManager.Instance.EffectSlider.value;     
    }

    public void Play_PointerSFX()
    {
        m_SFXSource.PlayOneShot(m_pointerClip);
    }
    public void Play_PressSFX()
    {
        m_SFXSource.PlayOneShot(m_ClickClip);
    }
}
