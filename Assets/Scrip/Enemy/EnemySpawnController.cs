using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemySpawnController : MonoBehaviour
{
    [SerializeField] protected int initEnemyPerWave = 5;
    [SerializeField] protected int currentEnemyPerWave;
    [SerializeField] protected int numEnemyPerwave = 2;   
    protected float spawnDelay = 3f; // delay thoi gian spawn Golem trong 1 wave  
    [SerializeField] protected float waveCoolDown = 10.0f; // Time in second between wave
     [SerializeField] protected bool isCoolDown;
    protected float coolDownCounter = 0;
   
}
