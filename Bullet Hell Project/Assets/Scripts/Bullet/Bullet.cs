using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables
    [Header("Bullet Data")]
    [SerializeField] BulletData bulletData;

    [SerializeField] float currentSpeed;
    float updateRateCounter;
    #endregion

    #region Unity Callback Function
    private void OnEnable() {
        GetComponentInChildren<SpriteRenderer>().sprite = bulletData.Sprite;
        ResetCounter();
    }

    private void Update() {
        if (HasLeftTheScreen()) {
            DestroyThisBullet();
        }

        Move();
        CountDownAndUpdate();
    }
    #endregion

    #region Functions
    private void Move() {
        transform.localPosition += transform.right * currentSpeed * Time.deltaTime;
    }

    private void ResetCounter() {
        updateRateCounter = bulletData.UpdateRate;
    }

    private void CountDownAndUpdate() {
        updateRateCounter -= Time.deltaTime;
        if (updateRateCounter <= 0f) {
            UpdateMovements();

            ResetCounter();
        }
    }

    private void UpdateMovements() {
        if (bulletData != null) {
            AddAcceleration();
            AddAngularVelocity();
        }
    }

    private void AddAcceleration() {
        currentSpeed = Mathf.Clamp(currentSpeed += bulletData.Acceleration, bulletData.MinSpeed, bulletData.MaxSpeed);
    }

    private void AddAngularVelocity() {
        transform.Rotate(Vector3.forward * bulletData.AngularVelocity * Time.deltaTime);
    }

    private bool HasLeftTheScreen() {
        float xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        float yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        if (transform.position.x < xMin || transform.position.x > xMax || transform.position.y < yMin || transform.position.y > yMax) {
            return true;
        }

        return false;
    }

    public void DestroyThisBullet() {
        gameObject.SetActive(false);
    }
    #endregion

    #region Getters and Setters
    public void SetInitialMovementDirection(Vector3 direction) {
        currentSpeed = bulletData.StartingSpeed;

        Vector3 vectorToTarget = direction - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    public void SetInitialMovementDirectionMoving(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        currentSpeed = bulletData.StartingSpeed;
    }

    public BulletData GetBulletData() {
        return bulletData;
    }

    public void SetBulletData(BulletData bulletData) {
        this.bulletData = bulletData;
    }
    #endregion
}
