using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance { get; set; }
    public GameObject allItems;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        { Destroy(gameObject); }
        else { Instance = this; }
    }
}