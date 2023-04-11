using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] float timeBetweenWaves = 3f;
    WaveConfig currentWave;
    public WaveConfig GetCurrentWave() => currentWave;

    [SerializeField] bool isLooping = false;

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
            foreach (WaveConfig wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartingWaypoint().position, Quaternion.Euler(0, 0, 180), transform);
                    yield return new WaitForSeconds(currentWave.GetSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping == true);                       
    }
}
