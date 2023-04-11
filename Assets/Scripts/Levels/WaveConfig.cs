using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] float timeBetweenSpawn = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minSpawnTime = 0.2f;

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
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
