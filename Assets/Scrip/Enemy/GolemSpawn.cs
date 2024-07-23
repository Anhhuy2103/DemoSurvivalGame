using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GolemSpawn : EnemySpawnController
{


    [SerializeField] private List<Enemy> currentGolemSAlive;
    [SerializeField] private GameObject GolemPrefab;


    private void Start()
    {


        currentEnemyPerWave = initEnemyPerWave;

        StartNextWave();
    }

    private void Update()
    {
        GetAllGolemDie();
    }
    //-----------------------------
    private void StartNextWave()
    {
        currentGolemSAlive.Clear();



        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentEnemyPerWave; i++)
        {
            // Tao random khoang cach vi tri cho GOlem spawn ra
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Instantiate the Golem
            var Golem = Instantiate(GolemPrefab, spawnPosition, quaternion.identity);

            // Get Enemy Script
            Enemy enemyScript = Golem.GetComponent<Enemy>();

            // Track this Golem
            currentGolemSAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);

        }
    }

    private void GetAllGolemDie()
    {

        // Get All GolemDead
        List<Enemy> GolemToRemove = new List<Enemy>();
        foreach (Enemy Golem in currentGolemSAlive)
        {
            if (Golem.isDead)
            {
                GolemToRemove.Add(Golem);
            }
        }

        // Actually remove All GolemDead
        foreach (Enemy Golem in GolemToRemove)
        {
            currentGolemSAlive.Remove(Golem);
        }

        GolemToRemove.Clear();

        // start cool down if all golem dead;
        if (currentGolemSAlive.Count == 0 && isCoolDown == false)
        {
            // start CD for nextwave
            StartCoroutine(WaveCoolDown());
        }

        // Run CD counter
        if (isCoolDown)
        {
            coolDownCounter -= Time.deltaTime;
        }
        else
        {
            coolDownCounter = waveCoolDown;
        }

    }

    private IEnumerator WaveCoolDown()
    {
        isCoolDown = true;
        yield return new WaitForSeconds(waveCoolDown);
        isCoolDown = false;
        currentEnemyPerWave = numEnemyPerwave;
        StartNextWave();
    }
}
