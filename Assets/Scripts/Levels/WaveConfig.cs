using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float timeBetweenSpawn = 1f;
    [SerializeField] private float spawnTimeVariance = 0f;
    [SerializeField] private float minSpawnTime = 0.2f;

    [SerializeField] private float waitForNextWave = 1f;
    public float GetWaitForNextWave() => waitForNextWave;

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public Transform GetLastWaypoint()
    {
        return pathPrefab.GetChild(pathPrefab.childCount - 1);
    }

    public IList<Transform> GetWaypoints()
    {
        IList<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab)
            waypoints.Add(child);
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public int GetEnemyCount() => enemyPrefabs.Count;
    public GameObject GetEnemyPrefab(int index) => enemyPrefabs[index];

    public float GetSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenSpawn - spawnTimeVariance, timeBetweenSpawn + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
    }
}
