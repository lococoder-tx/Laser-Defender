using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    int waveIndex;
    Coroutine spawnWaves;
    Coroutine spawnEnemies;

    IEnumerator Start()
    {
       do
       {
        yield return StartCoroutine(SpawnAllWaves());
       // Debug.Log("spawning waves again");
       }
       while(looping);
    }

    private IEnumerator SpawnAllWaves()
    {
         for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
         {
          var currentWave = waveConfigs[waveIndex];
          yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
         }
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int i = startingWave; i < waveConfig.GetNumofEnemies(); i++)
        {
           var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
           newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
           yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
         //Debug.Log("Done spawning all enemies in this wave. ");
    }
}
