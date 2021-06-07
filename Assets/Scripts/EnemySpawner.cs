using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    private WaveCleared waveCleared;
    private Enemy enemyBehaviour;
    [SerializeField] public int numWavesCleared=0;
    [SerializeField] float waveSpawnTime = 2f;
 
    // Start is called before the first frame update
    IEnumerator Start()
    {
        waveCleared = GameObject.FindObjectOfType<WaveCleared>();
        enemyBehaviour = GameObject.FindObjectOfType<Enemy>();
        do
        {
          yield return StartCoroutine(SpawnAllWaves());
            
        }

        while (looping);
    }

   private IEnumerator SpawnAllWaves()
    {

            for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
            {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemyInWave(currentWave));
            numWavesCleared++;
            Debug.Log("Current Wave = " + numWavesCleared);
            waveCleared.UpdateCurrentWave(numWavesCleared);
            if (numWavesCleared >= 10)
            {
                waveSpawnTime -= 0.1f;
            }
            yield return new WaitForSeconds(waveSpawnTime);

        }
        

    }
   private IEnumerator SpawnAllEnemyInWave(WaveConfig waveConfig)
    {
       
        for (int enemyCount = 0; enemyCount<waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
           var newEnemy =  Instantiate(waveConfig.GetEnemyPrefab(),
           waveConfig.GetEnemyWayPoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }

}
