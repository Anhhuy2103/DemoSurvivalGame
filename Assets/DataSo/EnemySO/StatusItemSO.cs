using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HPData", fileName = "DataSO")]
public class StatusItemSO : ScriptableObject
{
    public int CurrentHP = 100;
    public int MaxHP = 100;
    public int CurrentEXP = 5;
    public int maxHP = 100;
    public int BonusHP = 5;

    public int minusHealValue = 5;
}
