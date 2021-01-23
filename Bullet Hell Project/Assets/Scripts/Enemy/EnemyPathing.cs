using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    #region EnemyPathing Variables
    [Header("Enemy Wave Data")]
    [SerializeField]
    EnemyWaveData enemyWaveData;

    float timeBeforeMoveToNextWaypoint;
    int waypointIndex;
    #endregion

    #region Unity Callback Functions
    void OnEnable() {
        waypointIndex = 0;
        transform.position = enemyWaveData.GetWaypoints()[0].position;
        timeBeforeMoveToNextWaypoint = enemyWaveData.WaitTimeOnWaypoint;
    }

    void Update() {
        Move();
    }
    #endregion

    #region Functions
    private void Move() {
        if (waypointIndex <= enemyWaveData.GetWaypoints().Count - 1) {
            var targetPosition = enemyWaveData.GetWaypoints()[waypointIndex].transform.position;
            var movementThisFrame = enemyWaveData.EnemyMoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition) {
                timeBeforeMoveToNextWaypoint -= Time.deltaTime;
                if (timeBeforeMoveToNextWaypoint <= 0) {
                    timeBeforeMoveToNextWaypoint = enemyWaveData.WaitTimeOnWaypoint;
                    waypointIndex += 1;
                }
            }
        }
        else if (enemyWaveData.IsLoopingPath) {
            waypointIndex = 1;
        }
        else {
            GetComponent<Enemy>().DestroyThisEnemy();
        }
    }
    #endregion

    #region Setter
    public void SetEnemyWaveData(EnemyWaveData enemyWaveData) {
        this.enemyWaveData = enemyWaveData;
    }
    #endregion
}
