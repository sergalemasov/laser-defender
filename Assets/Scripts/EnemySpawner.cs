using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return initWaves();
        } while (looping);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator initWaves()
    {
        for (int waveConfigIndex = startingWave; waveConfigIndex < waveConfigs.Count; waveConfigIndex++)
        {
            yield return SpawnAllEnemiesInWave(waveConfigs[waveConfigIndex]);
        }
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            GameObject enemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );

            enemy.GetComponent<EnemyPathing>().Initialize(waveConfig);


            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
