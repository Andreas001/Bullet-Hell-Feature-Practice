using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPatternData", menuName = "Data/Pattern Data")]
public class PatternData : ScriptableObject
{
    #region Variables
    [Header("Settings")]
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] Vector3 spawnLocation;

    [Header("Bullet Settings")]
    [SerializeField] BulletData bulletData;
    [SerializeField] int amountOfBullets = 1;
    [SerializeField] int bulletStackAmount = 1;
    [SerializeField] float timeBetweenBulletStackSpawns = 0.5f;
    [SerializeField] int bulletSpreadAmount = 1;
    [SerializeField] float spreadDistance = 0.3f;
    [SerializeField] bool aimedAtPlayer = false;

    [Header("Firing Angle Settings")]
    [SerializeField] float circleRadius = 0.1f;
    [SerializeField] float circleStartAngle = 180f, circleEndAngle = 180f;
    [SerializeField] float circleAngleModifier = 0f; //Needed to add modify circleStartangle and circleEndangle to create a changing angle effect
    [SerializeField] float randomRange = 0f; //Will be used to give spawn locations a bit of variety by adding its angle with Random.Range(-randomRange, randomRange);    
    #endregion

    #region Getters and Setters
    public float FireRate { get => fireRate; set => fireRate = value; }
    public Vector3 SpawnLocation { get => spawnLocation; set => spawnLocation = value; }
    public BulletData BulletData { get => bulletData; set => bulletData = value; }
    public int AmountOfBullets { get => amountOfBullets; set => amountOfBullets = value; }
    public int BulletStackAmount { get => bulletStackAmount; set => bulletStackAmount = value; }
    public float TimeBetweenBulletStackSpawns { get => timeBetweenBulletStackSpawns; set => timeBetweenBulletStackSpawns = value; }
    public int BulletSpreadAmount { get => bulletSpreadAmount; set => bulletSpreadAmount = value; }
    public float SpreadDistance { get => spreadDistance; set => spreadDistance = value; }
    public bool AimedAtPlayer { get => aimedAtPlayer; set => aimedAtPlayer = value; }

    public float CircleRadius { get => circleRadius; set => circleRadius = value; }
    public float CircleStartAngle { get => circleStartAngle; set => circleStartAngle = value; }
    public float CircleEndAngle { get => circleEndAngle; set => circleEndAngle = value; }
    public float CircleAngleModifier { get => circleAngleModifier; set => circleAngleModifier = value; }
    public float RandomRange { get => randomRange; set => randomRange = value; }
    #endregion
}
