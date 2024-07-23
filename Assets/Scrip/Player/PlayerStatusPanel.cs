using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusPanel : MonoBehaviour
{ 
    [SerializeField] private Text HP;
    [SerializeField] private Text Enegy;
    [SerializeField] private Text EXP;
    [SerializeField] private Text Level;

    private void Update()
    {
        updateStatusText();
    }
    private void updateStatusText()
    {
        HP.text = $"HP: {PlayerStatusManager.Instance.playerdataSo.CurrentHP} / {PlayerStatusManager.Instance.playerdataSo.maxHP}";
        Enegy.text = $"Enegy: {PlayerStatusManager.Instance.playerdataSo.CurrentEnegy} / {PlayerStatusManager.Instance.playerdataSo.maxEnegy}";
        EXP.text = $"EXP: {PlayerStatusManager.Instance.playerdataSo.CurrentEXP} / {PlayerStatusManager.Instance.playerdataSo.maxEXP}";
        Level.text = $"LV: {PlayerStatusManager.Instance.playerdataSo.playerLevel}";
       
    }
}
