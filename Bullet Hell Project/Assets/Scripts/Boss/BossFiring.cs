using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFiring : MonoBehaviour
{
    #region Variables
    [Header("Pattern Spawner Pools")]
    [SerializeField] GameObject patternSpawnerPool;
    [SerializeField] List<GameObject> activePatternSpawners;

    [Header("Bullet Pools")]
    [SerializeField] GameObject bulletPools;

    float startFiringAfterSeconds;
    int currentPhase;
    float patternDurationCounter;
    int currentPattern;
    bool hasSpawnedPatternSpawners;
    float nextPatternCounter;

    #endregion

    #region Unity Callback Functions
    private void Start() {
        startFiringAfterSeconds = GetComponent<Boss>().GetBossData().StartFiringAfterSeconds;
        currentPhase = GetComponent<Boss>().GetBossData().StartingPhase;
        currentPattern = 0;
        patternDurationCounter = GetComponent<Boss>().GetBossData().Phases[currentPhase].PatternSetData[0].PatternSetDuration;
        hasSpawnedPatternSpawners = false;
        nextPatternCounter = GetComponent<Boss>().GetBossData().Phases[currentPhase].PatternSetData[currentPattern].TimeUntilNextPattern;
    }

    private void OnDisable() {
        DisablePatternSpawners();
    }

    private void Update() {
        if(startFiringAfterSeconds > 0) {
            startFiringAfterSeconds -= Time.deltaTime;
        }
        else {
            if(GetComponent<Boss>().GetHealth() <= GetComponent<Boss>().GetBossData().Phases[currentPhase].ChangePhaseAtThisHealth) {
                currentPhase += 1; // Next Phase

                if(currentPhase >= GetComponent<Boss>().GetBossData().Phases.Count) {
                    currentPhase = 0;
                }
            }

            if(patternDurationCounter > 0) {
                if(nextPatternCounter >= 0) {
                    nextPatternCounter -= Time.deltaTime;
                }              

                if (nextPatternCounter <= 0) {
                    patternDurationCounter -= Time.deltaTime;

                    if (!hasSpawnedPatternSpawners) {
                        SpawnPatternSpawners();

                        hasSpawnedPatternSpawners = true;
                    }
                }
            }
            else if (patternDurationCounter <= 0 && hasSpawnedPatternSpawners) {
                DisablePatternSpawners();
                hasSpawnedPatternSpawners = false;

                currentPattern += 1;

                if (currentPattern >= GetComponent<Boss>().GetBossData().Phases[currentPhase].PatternSetData.Count) {
                    currentPattern = 0;
                }

                patternDurationCounter = GetComponent<Boss>().GetBossData().Phases[currentPhase].PatternSetData[currentPattern].PatternSetDuration;
                nextPatternCounter = GetComponent<Boss>().GetBossData().Phases[currentPhase].PatternSetData[currentPattern].TimeUntilNextPattern;
            }
        }
    }
    #endregion

    #region Functions
    private void SpawnPatternSpawners() {
        foreach (PatternData patternData in GetComponent<Boss>().GetBossData().Phases[currentPhase].PatternSetData[currentPattern].PatternSets) {
            GameObject patternSpawner = patternSpawnerPool.GetComponent<ObjectPool>().GetObject();
            activePatternSpawners.Add(patternSpawner);

            patternSpawner.GetComponent<PatternSpawner>().SetPatternData(patternData);
            patternSpawner.GetComponent<PatternSpawner>().SetBulletPools(bulletPools);

            patternSpawner.SetActive(true);
        }
    }

    public void DisablePatternSpawners() {
        List<GameObject> patternSpawnersToRemove = new List<GameObject>();
        foreach (GameObject patternSpawner in activePatternSpawners) {
            patternSpawner.SetActive(false);
            patternSpawnersToRemove.Add(patternSpawner);
        }
        //Split into two as to not change activePatternSpawner while its looping through it

        foreach (GameObject patternSpawner in patternSpawnersToRemove) {
            activePatternSpawners.Remove(patternSpawner);
        }
    }
    #endregion

    #region Getters and Setters

    #endregion
}
