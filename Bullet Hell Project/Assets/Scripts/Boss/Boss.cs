using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    #region Variables
    [Header("Boss Data")]
    [SerializeField] BossData bossData;

    [Header("Stats")]
    [SerializeField] float health;
    #endregion

    #region Unity Callback Functions
    private void Start() {
        health = bossData.Health;
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
    public float GetHealth() {
        return health;
    }

    public BossData GetBossData() {
        return bossData;
    }

    public void SetBossData(BossData bossData) {
        this.bossData = bossData;
    }
    #endregion

}
