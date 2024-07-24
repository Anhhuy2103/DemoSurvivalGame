using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Guns;

public class GlobalReferences : MonoBehaviour
{

    // this c# make bulletImpactEffect
    public static GlobalReferences Instance { get; set; }

    [Header("Bool")]
    public bool isActiveWeaponbro;
    public bool isActiveEquipnbro;

    [Header("Guns Effect")]
    public GameObject StonebulletImpactEffectPrefab;
    public GameObject Grenade_PlasmaExplosion_bulletImpactEffectPrefab;
    public GameObject BloodEffectPrefab;
    public GameObject grenadeExplosionEffect;
    public GameObject smokeExplosionEffect;

    [Header("Chops Effect")]
    public GameObject Chop_WoodStoneEffect_Prefab;

    [Header("Resources Status")]
    public int TreeCurrentHP;
    public int TreeMaxHP;

    public int StoneHP;
    public int StoneMaxHP;

    [Header("Enemy Status")]
    public int EnemyCurrentHP;
    public int EnemyMaxHP;


    [Header("TimeDecay")]
    public float tickTime;
    public float MaxtickTime;

    [Header("Item Status")]
    public int itemCurrentHP;
    public int itemMaxtHP;
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
