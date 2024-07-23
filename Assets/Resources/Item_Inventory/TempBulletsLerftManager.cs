using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TempBulletsLerftManager : MonoBehaviour
{
    public static TempBulletsLerftManager Instance { get; set; }

    public int SCI_bullets_afterDestroy;
    public int Grenade_bullets_afterDestroy;

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
}
