using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CheckPoint", menuName = "ScriptableObject/CheckPoint", order = 1)]
public class CheckPoints : ScriptableObject
{
    public string checkpointName;
    public bool isCompleted;
}
