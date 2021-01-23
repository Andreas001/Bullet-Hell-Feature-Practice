using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBulletData", menuName = "Data/Bullet Data")]
public class BulletData : ScriptableObject
{
    #region Bullet Variables
    [Header("Damage Stats")]
    [SerializeField] float damage = 100;

    [Header("Movement Stats")]
    [SerializeField] float updateRate = 0.1f;
    [SerializeField] float startingSpeed = 2f;
    [SerializeField] float acceleration = 0f;
    [SerializeField] float minSpeed = 0f;
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float angularVelocity = 0f;

    [Header("Graphic")]
    [SerializeField] Sprite sprite;

    [Header("Collider Shape")]
    [SerializeField] string shape = "circle";
    #endregion

    #region Getters and Setters
    public float Damage { get => damage; set => damage = value; }

    public float UpdateRate { get => updateRate; set => updateRate = value; }
    public float StartingSpeed { get => startingSpeed; set => startingSpeed = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }
    public float MinSpeed { get => minSpeed; set => minSpeed = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float AngularVelocity { get => angularVelocity; set => angularVelocity = value; }

    public Sprite Sprite { get => sprite; set => sprite = value; }

    public string Shape { get => shape; set => shape = value; }
    #endregion
}
