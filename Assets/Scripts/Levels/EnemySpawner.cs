using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    WaveConfig currentWave;
    public WaveConfig GetCurrentWave() => currentWave;

    [SerializeField] bool isLooping = false;
    [SerializeField] private int startWaveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            for (int i = startWaveIndex; i < waveConfigs.Count; i++) 
            {
                currentWave = waveConfigs[i];
                for (int j = 0; j < currentWave.GetEnemyCount(); j++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(j),
                        currentWave.GetStartingWaypoint().position, Quaternion.Euler(0, 0, 180), transform);
                    yield return new WaitForSeconds(currentWave.GetSpawnTime());
                }
                yield return new WaitForSeconds(currentWave.GetWaitForNextWave());
            }
        } while (isLooping == true);                    
    }
}
