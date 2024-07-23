using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData",fileName ="DataSO")]
public class PlayerDataSO : ScriptableObject
{
    public int CurrentHP = 100;
    public int CurrentEnegy = 100;
    public int CurrentEXP = 5;

    public int maxHP = 100;
    public int maxEnegy = 100;
    public int maxEXP = 10;
    public int maxMoney = 999999;

    public float WalkSpeed = 4;
    public float RunSpeed = 6;
    public float CrouchSpeed = 2;

    
    public int playerLevel = 0;
    public int MaxLevel  = 999;

    public int CurrentCoin = 1000 ;
    public int playerHunger;
    public int playerDame;
    public bool isLevelUp;
}
