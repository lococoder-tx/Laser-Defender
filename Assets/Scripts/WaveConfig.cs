using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField]  GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = .5f;
    [SerializeField] float spawnRandomFactor = .3f;
    [SerializeField] int numOfEnemies = 5;
    [SerializeField] float moveSpeed = 5f;

    public GameObject GetEnemyPrefab()
    {
        return EnemyPrefab;
    }

     public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
            waveWaypoints.Add(child);
        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float GetspawnRandomFactor()
    {
        return spawnRandomFactor;
    }

    public int GetNumofEnemies()
    {
        return numOfEnemies;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
