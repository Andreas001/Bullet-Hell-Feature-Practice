using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Variables
    [Header("Enemy Data")]
    [SerializeField] EnemyData enemyData;

    [Header("Stats")]
    [SerializeField] float health;
    #endregion

    #region Unity Callback Functions
    private void OnEnable() {
        health = enemyData.Health;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet) {
            bullet.DestroyThisBullet();
            health = health - bullet.GetBulletData().Damage;

            if (health <= 0) {
                DestroyThisEnemy();
            }
        }
    }
    #endregion

    #region Functions
    public void DestroyThisEnemy() {
        gameObject.SetActive(false);
    }
    #endregion

    #region Getters and Setters
    public EnemyData GetEnemyData() {
        return enemyData;
    }

    public void SetEnemyData(EnemyData enemyData) {
        this.enemyData = enemyData;
    }
    #endregion
}
