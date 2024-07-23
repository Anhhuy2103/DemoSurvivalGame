using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questName;
    public string questGiver;
    public string questDecription;

    [Header("Bool")]

    public bool Accepted;

    public bool Decline;

    public bool InitDiablogCompleted;

    public bool IsCompleted;

    public bool HasNoRequirement;

    [Header("Quest Info")]
    public QuestInfo info;
}

