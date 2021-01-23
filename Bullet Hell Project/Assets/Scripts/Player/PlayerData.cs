using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    #region Variables
    [Header("Player Stats")]
    [SerializeField] float health = 500f;

    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float padding = 1f;

    [Header("Player Projectile")]
    //[SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] Vector2 projectileFiringDirection = new Vector2(0, 1);
    [SerializeField] BulletData playerBulletData;

    [Header("Player Sfx")]
    [SerializeField] AudioClip shootSfx;
    [SerializeField] [Range(0, 1)] float shootSfxVolume = 0.25f;
    [SerializeField] AudioClip deathSfx;
    [SerializeField] [Range(0, 1)] float deathSfxVolume = 0.7f;

    [Header("Player Vfx")]
    [SerializeField] GameObject deathVfxPrefab;
    [SerializeField] float deathduration = 1f;
    #endregion

    #region Getters
    public float GetHealth() {
        return health;
    }

    public float GetMoveSpeed() {
        return moveSpeed;
    }

    public float GetPadding() {
        return padding;
    }

    public float GetProjectileFiringPeriod() {
        return projectileFiringPeriod;
    }

    public Vector2 GetProjectileFiringDirection() {
        return projectileFiringDirection;
    }

    public BulletData GetPlayerBulletData() {
        return playerBulletData;
    }

    public AudioClip GetShootAudioClip() {
        return shootSfx;
    }

    public float GetShootAudioVolume() {
        return shootSfxVolume;
    }

    public AudioClip GetDeathAudioClip() {
        return deathSfx;
    }

    public float GetDeathAudioVolume() {
        return deathSfxVolume;
    }

    public GameObject GetDeathVfxPrefab() {
        return deathVfxPrefab;
    }

    public float GetDeathDuration() {
        return deathduration;
    }
    #endregion
}
