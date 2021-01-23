using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyWaveData", menuName = "Data/Enemy Wave Data")]
public class EnemyWaveData : ScriptableObject {
    #region Variables
    [Header("Waypoint / Path Prefab")]
    [SerializeField] GameObject pathPrefab;
    
    [Header("Spawn Settings")]
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomfactor = 0.3f;

    [Header("Enemy Settings")]
    [SerializeField] EnemyData enemyData;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float enemyMoveSpeed = 2f;

    [Header("Pathing Settings")]
    [SerializeField] float waitTimeOnWaypoint = 0.1f;
    [SerializeField] bool isLoopingPath;

    [Header("Others")]
    [SerializeField] float timeUntilNextWave = 1f;  
    #endregion

    #region Getters
    public List<Transform> GetWaypoints() {
        var waveWayPoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform) {
            waveWayPoints.Add(child);
        }

        return waveWayPoints;
    }

    public float TimeBetweenSpawns { get => timeBetweenSpawns; set => timeBetweenSpawns = value; }
    public float SpawnRandomfactor { get => spawnRandomfactor; set => spawnRandomfactor = value; }

    public EnemyData EnemyData { get => enemyData; set => enemyData = value; }
    public int NumberOfEnemies { get => numberOfEnemies; set => numberOfEnemies = value; }
    public float EnemyMoveSpeed { get => enemyMoveSpeed; set => enemyMoveSpeed = value; }

    public float WaitTimeOnWaypoint { get => waitTimeOnWaypoint; set => waitTimeOnWaypoint = value; }
    public bool IsLoopingPath { get => isLoopingPath; set => isLoopingPath = value; }

    public float TimeUntilNextWave { get => timeUntilNextWave; set => timeUntilNextWave = value; }
    #endregion
}
