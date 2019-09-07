using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(initWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator initWaves()
    {
        foreach (WaveConfig waveConfig in waveConfigs)
        {
            yield return SpawnAllEnemiesInWave(waveConfig);
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
