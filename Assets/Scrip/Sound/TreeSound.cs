using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSound : MonoBehaviour
{
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioClip TreeFall_SFX;

    public void Play_TreeFall()
    {
        SFXSource.PlayOneShot(TreeFall_SFX);
    }
}
