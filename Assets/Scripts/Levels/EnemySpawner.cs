using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    WaveConfig currentWave;
    public WaveConfig GetCurrentWave() => currentWave;
    private GameObject currentWaveLastShip = null;
    private Transform currentWaveLastWaypoint = null;
    private bool currentWaveEnds = false;

    [SerializeField] bool isLooping = false;
    [SerializeField] private int startWaveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
        Health.OnDeathEvent += Health_OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        //Checking if the current wave's last ship reachs the end
        if (currentWaveLastShip != null)
        {
            Vector3 offset = currentWaveLastShip.transform.position - currentWaveLastWaypoint.position;

            if (Mathf.Approximately(offset.sqrMagnitude, 0f))             
                currentWaveEnds = true;               
        }
    }

    //When the last enemy gets destroyed, the wave should end immediately
    private void Health_OnDeath(object sender, EventArgs e)
    {
        currentWaveEnds = true;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            for (int i = startWaveIndex; i < waveConfigs.Count; i++) 
            {
                currentWave = waveConfigs[i];
                currentWaveLastWaypoint = currentWave.GetLastWaypoint();
                for (int j = 0; j < currentWave.GetEnemyCount(); j++)
                {
                    GameObject enemyShip = Instantiate(currentWave.GetEnemyPrefab(j),
                        currentWave.GetStartingWaypoint().position, Quaternion.Euler(0, 0, 180), transform);
                    if (j == currentWave.GetEnemyCount() - 1) //Take reference to the last enemy of the wave
                        currentWaveLastShip = enemyShip;
                    yield return new WaitForSeconds(currentWave.GetSpawnTime());
                }
                yield return new WaitUntil(() => currentWaveEnds);
                currentWaveLastShip = null;
                currentWaveLastWaypoint = null;
                currentWaveEnds = false;
                yield return new WaitForSeconds(currentWave.GetWaitForNextWave());
            }
        } while (isLooping == true);                    
    }
}
