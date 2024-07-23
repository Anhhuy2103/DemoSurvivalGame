using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawnWhenAllClear : MonoBehaviour
{
    public int initGolemsPerWave = 5;
    public int currentGolemsPerWave;
    [SerializeField] private int numGolemPerwave = 2;


    public float spawnDelay = 1f; // delay thoi gian spawn Golem trong 1 wave

    public int currentWave = 0;
    public float waveCoolDown = 10.0f; // Time in second between wave

    public bool isCoolDown;
    public float coolDownCounter = 0;

    public List<Enemy> currentGolemsAlive;
    public GameObject GolemPrefab;

    [SerializeField] private Image waveOverUIPanel;
    [SerializeField] private TextMeshProUGUI waveOverUI;
    [SerializeField] private TextMeshProUGUI CoolDownCounterUI;
    [SerializeField] private TextMeshProUGUI CurrentWaveUI;

    private void Start()
    {
        

        currentGolemsPerWave = initGolemsPerWave;
        waveOverUI.gameObject.SetActive(false);
        waveOverUIPanel.gameObject.SetActive(false );
        CoolDownCounterUI.gameObject.SetActive(false);
        StartNextWave();
    }

    private void Update()
    {
        GetAllGolemDie();
    }
    //-----------------------------
    private void StartNextWave()
    {
        currentGolemsAlive.Clear();
        currentWave++;

        

        CurrentWaveUI.text = "Wave: " + currentWave.ToString();
         StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for(int i = 0; i < currentGolemsPerWave; i++)
        {
            // Tao random khoang cach vi tri cho GOlem spawn ra
            Vector3 spawnOffset = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f,1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            // Instantiate the Golem
            var Golem = Instantiate(GolemPrefab, spawnPosition, quaternion.identity);

            // Get Enemy Script
            Enemy enemyScript = Golem.GetComponent<Enemy>();

            // Track this Golem
            currentGolemsAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);

        }
    }

    private void GetAllGolemDie()
    {

        // Get All GolemDead
        List<Enemy> GolemToRemove = new List<Enemy>();
        foreach(Enemy Golem in currentGolemsAlive)
        {
            if (Golem.isDead) 
            {
                GolemToRemove.Add(Golem);
            }
        }

        // Actually remove All GolemDead
        foreach(Enemy Golem in GolemToRemove)
        {
            currentGolemsAlive.Remove(Golem);
        }

        GolemToRemove.Clear();

        // start cool down if all golem dead;
        if(currentGolemsAlive.Count == 0 && isCoolDown == false)
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
        CoolDownCounterUI.text = coolDownCounter.ToString("F0");
    }

    private IEnumerator WaveCoolDown()
    {
        isCoolDown = true;
        waveOverUI.gameObject.SetActive(true);
        waveOverUIPanel.gameObject.SetActive(true);
        CoolDownCounterUI.gameObject.SetActive(true );
        SoundManager.Instance.musicSource2.Stop();
        SoundManager.Instance.musicSource.Play();

        yield return new WaitForSeconds(waveCoolDown);
        waveOverUI.gameObject.SetActive(false);
        waveOverUIPanel.gameObject.SetActive(false);
        CoolDownCounterUI.gameObject.SetActive(false);

        isCoolDown = false;

        currentGolemsPerWave *= numGolemPerwave;
        StartNextWave();
    }
}
