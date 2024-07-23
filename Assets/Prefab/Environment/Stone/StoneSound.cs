using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSound : MonoBehaviour
{
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioClip StoneFall_SFX;

    public void Play_StoneFall()
    {
        SFXSource.PlayOneShot(StoneFall_SFX);
    }
}
