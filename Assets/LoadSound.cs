using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSound : MonoBehaviour
{
    [SerializeField] private AudioSource load_AudioSource;

    private void inItSetVolume()
    {
        load_AudioSource.volume = SettingManager.Instance.musicSlider.value;
    }

    private void Update()
    {
        inItSetVolume();
    }
}
