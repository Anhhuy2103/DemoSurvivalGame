using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int[] playerStats; // [0]-heal, [1] - enegy , [2] Money
    public float[] PlayerPositionAndRotation;  // position :[0] x, [1] y ,[2] z and rotation:[3] x ,[4] y,[5] z
    public string[] inventoryContent; 
    public string[] quickSlotContent; 

    public PlayerData(int[] _playerStats, float[] _playerPosAndRot, string[] _inventoryContent, string[] _quickslotContent) 
    { 
        playerStats = _playerStats;
        PlayerPositionAndRotation = _playerPosAndRot;
        inventoryContent = _inventoryContent; 
        quickSlotContent = _quickslotContent;
    }

}