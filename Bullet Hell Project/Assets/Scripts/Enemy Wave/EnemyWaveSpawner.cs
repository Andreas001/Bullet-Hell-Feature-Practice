using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour {
    #region Variables
    [Header("Enemy Waves")]
    [SerializeField] List<EnemyWaveData> enemyWaveDatas;

    [Header("Object Pools")]
    [SerializeField] GameObject enemyPools;
    [SerializeField] GameObject bulletPools;

    [Header("Spawner Settings")]
    [SerializeField] float secondsBeforeStartSpawning = 2f;
    [SerializeField] bool looping = false;
    [SerializeField] int startingWave = 0;

    public GameObject boss;
    #endregion

    #region Unity Callback Functions
    IEnumerator Start() {
        do {
            yield return new WaitForSeconds(secondsBeforeStartSpawning);
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }
    #endregion

    #region Functions
    private IEnumerator SpawnAllWaves() {
        for (int waveIndex = startingWave; waveIndex < enemyWaveDatas.Count; waveIndex++) {
            var currentWave = enemyWaveDatas[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            yield return new WaitForSeconds(currentWave.TimeUntilNextWave);
        }

        boss.SetActive(true);
    }

    private IEnumerator SpawnAllEnemiesInWave(EnemyWaveData enemyWaveData) {
        for (int enemyCount = 0; enemyCount < enemyWaveData.NumberOfEnemies; enemyCount++) {
            GameObject enemy;

            switch (enemyWaveData.EnemyData.Shape) {
                case "small":
                    enemy = enemyPools.transform.Find("Small Enemy Pool").GetComponent<ObjectPool>().GetObject();
                    break;
                case "big":
                    enemy = enemyPools.transform.Find("Big Enemy Pool").GetComponent<ObjectPool>().GetObject();
                    break;
                default:
                    enemy = enemyPools.transform.Find("Small Enemy Pool").GetComponent<ObjectPool>().GetObject();
                    break;
            }

            enemy.GetComponent<Enemy>().SetEnemyData(enemyWaveData.EnemyData);
            enemy.GetComponent<EnemyPathing>().SetEnemyWaveData(enemyWaveData);
            enemy.GetComponent<EnemyFiring>().SetBulletPools(bulletPools);

            enemy.SetActive(true);

            yield return new WaitForSeconds(enemyWaveData.TimeBetweenSpawns);
        }
    }
    #endregion
}
