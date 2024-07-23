using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Quest Data", menuName = "QuestSO/Quest Info", order = 1)]
public class QuestInfo : ScriptableObject
{
    [TextArea(5,10)]
    public List<string> initialDialog;

    [Header("Option")]
    [TextArea(5, 10)]
    public string accepOption;

    [TextArea(5, 10)]
    public string acceptAnswer;

    [TextArea(5, 10)]
    public string declineOption;

    [TextArea(5, 10)]
    public string declineAnswer;

    [TextArea(5, 10)]
    public string comebackAfterDecline;

    [TextArea(5, 10)]
    public string comebackInprogress;

    [TextArea(5, 10)]
    public string combackComplete;

    [TextArea(5, 10)]
    public string finalWords;

    // -------------- REWARD -------------

    [Header("Rewards")]
    public int coinReward;
    public string rewardItem_1;
    public string rewardItem_2;
   

    // ============= REQUIREMENTS ===========

    [Header("Requirement")]
    public string firstrequirementItem;
    public int firstRequirementAmount;

    public string SecondrequirementItem;
    public int SecondRequirementAmount;

    public bool hasCheckpoints;
    public List<CheckPoints> checkpoints;
}
