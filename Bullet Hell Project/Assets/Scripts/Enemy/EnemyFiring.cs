using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    #region Variables
    [Header("Bullet Pools")]
    [SerializeField] GameObject bulletPools;

    Coroutine firingCoroutine;
    float fireRateCounter;
    float tempStartingCircleAngle;
    float tempEndingCircleAngle;
    #endregion

    #region Unity Callback Functions
    private void OnEnable() {
        tempStartingCircleAngle = GetComponent<Enemy>().GetEnemyData().CircleStartAngle;
        tempEndingCircleAngle = GetComponent<Enemy>().GetEnemyData().CircleEndAngle;
        ResetShootCounter();
    }

    private void OnDisable() {
        if(firingCoroutine != null) {
            StopCoroutine(firingCoroutine);
        }
    }

    private void Update() {
        CountDownAndShoot();
    }
    #endregion

    #region Functions
    private void ResetShootCounter() {
        if (GetComponent<Enemy>().GetEnemyData().RandomFireRate) {
            fireRateCounter = Random.Range(GetComponent<Enemy>().GetEnemyData().MinFireRate, GetComponent<Enemy>().GetEnemyData().MaxFireRate);
        }
        else {
            fireRateCounter = GetComponent<Enemy>().GetEnemyData().MaxFireRate;
        }
    }

    private void CountDownAndShoot() {
        fireRateCounter -= Time.deltaTime;
        if (fireRateCounter <= 0f) {
            SpawnBulletPattern();

            tempStartingCircleAngle += GetComponent<Enemy>().GetEnemyData().CircleAngleModifier;
            tempEndingCircleAngle += GetComponent<Enemy>().GetEnemyData().CircleAngleModifier;

            ResetShootCounter();
        }
    }

    private void SpawnBulletPattern() {
        EnemyData enemyData = GetComponent<Enemy>().GetEnemyData();

        float angleStep = (tempEndingCircleAngle - tempStartingCircleAngle) / enemyData.AmountOfBullets;
        float angle = tempStartingCircleAngle; // + enemyData.CircleAngleModifier;

        for (int bulletIndex = 0; bulletIndex < enemyData.AmountOfBullets; bulletIndex++) {
            //Adds a bit of randomness to the angle (Ex: angle is 30 then the random will add 3 or -3 so the angle will be 27 or 33)
            float anglePlusRandom = angle + Random.Range(-enemyData.RandomRange, enemyData.RandomRange);

            float startingSpreadAngle = anglePlusRandom - enemyData.SpreadDistance;
            float endingSpreadAngle = anglePlusRandom + enemyData.SpreadDistance;

            float angleStepSpread = (endingSpreadAngle - startingSpreadAngle) / enemyData.BulletSpreadAmount;
            float angleSpread = startingSpreadAngle;

            for (int spreadIndex = 0; spreadIndex < enemyData.BulletSpreadAmount; spreadIndex++) {
                float bulletDirectionX = transform.position.x + Mathf.Sin((angleSpread * Mathf.PI) / 180f) * enemyData.CircleRadius;
                float bulletDirectionY = transform.position.y + Mathf.Cos((angleSpread * Mathf.PI) / 180f) * enemyData.CircleRadius;

                Vector3 bulletMoveVector = new Vector3(bulletDirectionX, bulletDirectionY, 0f);
                Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized * enemyData.BulletData.StartingSpeed;

                firingCoroutine = StartCoroutine(SpawnBullets(bulletMoveVector, bulletDirection, enemyData));

                angleSpread += angleStepSpread;
            }

            angle += angleStep;
        }
    }

    IEnumerator SpawnBullets(Vector3 bulletMoveVector, Vector2 bulletDirection, EnemyData enemyData) {
        for (int y = 0; y < enemyData.BulletStackAmount; y++) {
            GameObject bulletPoolAccordingToShape = GetBulletPoolAccordingToShape(enemyData.BulletData.Shape);
            GameObject bullet = bulletPoolAccordingToShape.GetComponent<ObjectPool>().GetObject();
            bullet.transform.position = bulletMoveVector;
            bullet.GetComponent<Bullet>().SetBulletData(enemyData.BulletData);
            bullet.GetComponent<Bullet>().SetInitialMovementDirectionMoving(bulletDirection);
            bullet.SetActive(true);
            yield return new WaitForSeconds(enemyData.TimeBetweenBulletStackSpawns);
        }
    }

    private GameObject GetBulletPoolAccordingToShape(string shape) {
        switch (shape) {
            case "circle":
                return bulletPools.transform.Find("Circle Bullet Pool").gameObject;
            case "oval":
                return bulletPools.transform.Find("Oval Bullet Pool").gameObject;
            case "triangle":
                return bulletPools.transform.Find("Triangle Bullet Pool").gameObject;
        }

        return bulletPools.transform.Find("Circle Bullet Pool").gameObject;
    }
    #endregion

    #region Getters and Setters
    public void SetBulletPools(GameObject bulletPools) {
        this.bulletPools = bulletPools;
    }
    #endregion
}
