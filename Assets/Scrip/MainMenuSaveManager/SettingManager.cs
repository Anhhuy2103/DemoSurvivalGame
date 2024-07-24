using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using static SaveManager;

public class SettingManager : MonoBehaviour
{
    public Button backButton;

    [Header("Sound Setting")]

    public Slider musicSlider;

    public Slider EffectSlider;

    Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown resolutionDropDown;





    public static SettingManager Instance { get; set; }
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
        BackButtonMenthod();
        inItResolution();
    }
    private void BackButtonMenthod()
    {
        backButton.onClick.AddListener(() =>
        {
            SaveManager.Instance.SavedVolumeSetting( musicSlider.value, EffectSlider.value);
            Debug.Log("Save Volume to player Preb");
        });

        StartCoroutine(LoadAndApplySetting());
    }

    private IEnumerator LoadAndApplySetting()
    {
        LoadAndSetVolume();
        // load graphic setting or load something;
        yield return new WaitForSeconds(0.2f);
    }
    private void LoadAndSetVolume()
    {
        VolumeSettings volumeSettings = SaveManager.Instance.LoadVolumeSettings();
        musicSlider.value = volumeSettings.music;
        EffectSlider.value = volumeSettings.effect;

        Debug.Log("Load Sound Value");
    }

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixer SFXMixer;
    public void SetVolum(float volume)
    {
        audioMixer.SetFloat("Volume", EffectSlider.value);
        SFXMixer.SetFloat("Volume", musicSlider.value);
    }

    public void SetQualityGraphic(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScene(bool isFullScene)
    {
        Screen.fullScreen = isFullScene;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    private void inItResolution()
    {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                (resolutions[i].height == Screen.currentResolution.height))
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
}
