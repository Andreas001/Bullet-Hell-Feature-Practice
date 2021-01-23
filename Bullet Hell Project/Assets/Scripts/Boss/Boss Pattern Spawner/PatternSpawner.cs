using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSpawner : MonoBehaviour {
    #region Variables
    [Header("Pattern Data")]
    [SerializeField] PatternData patternData;

    [Header("Bullet Pools")]
    [SerializeField] GameObject bulletPools;

    Coroutine firingCoroutine;
    float fireRateCounter;
    float tempStartingCircleAngle;
    float tempEndingCircleAngle;
    #endregion

    #region Unity Callback Function
    private void OnEnable() {
        tempStartingCircleAngle = patternData.CircleStartAngle;
        tempEndingCircleAngle = patternData.CircleEndAngle;
        ResetShootCounter();
    }

    private void OnDisable() {
        if (firingCoroutine != null) {
            StopCoroutine(firingCoroutine);
        }
    }

    private void Update() {
        CountDownAndShoot();
    }
    #endregion

    #region Functions
    private void ResetShootCounter() {
        fireRateCounter = patternData.FireRate;
    }

    private void CountDownAndShoot() {
        fireRateCounter -= Time.deltaTime;
        if (fireRateCounter <= 0f) {
            SpawnBulletPattern();

            tempStartingCircleAngle += patternData.CircleAngleModifier;
            tempEndingCircleAngle += patternData.CircleAngleModifier;

            ResetShootCounter();
        }
    }

    private void SpawnBulletPattern() {
        float angleStep = (tempEndingCircleAngle - tempStartingCircleAngle) / patternData.AmountOfBullets;
        float angle = tempStartingCircleAngle; // + enemyData.CircleAngleModifier;

        for (int bulletIndex = 0; bulletIndex < patternData.AmountOfBullets; bulletIndex++) {
            //Adds a bit of randomness to the angle (Ex: angle is 30 then the random will add 3 or -3 so the angle will be 27 or 33)
            float anglePlusRandom = angle + Random.Range(-patternData.RandomRange, patternData.RandomRange);

            float startingSpreadAngle = anglePlusRandom - patternData.SpreadDistance;
            float endingSpreadAngle = anglePlusRandom + patternData.SpreadDistance;

            float angleStepSpread = (endingSpreadAngle - startingSpreadAngle) / patternData.BulletSpreadAmount;
            float angleSpread = startingSpreadAngle;

            for (int spreadIndex = 0; spreadIndex < patternData.BulletSpreadAmount; spreadIndex++) {
                float bulletDirectionX = patternData.SpawnLocation.x + Mathf.Sin((angleSpread * Mathf.PI) / 180f) * patternData.CircleRadius;
                float bulletDirectionY = patternData.SpawnLocation.y + Mathf.Cos((angleSpread * Mathf.PI) / 180f) * patternData.CircleRadius;

                Vector3 bulletMoveVector = new Vector3(bulletDirectionX, bulletDirectionY, 0f);
                Vector2 bulletDirection = (bulletMoveVector - patternData.SpawnLocation).normalized * patternData.BulletData.StartingSpeed;

                firingCoroutine = StartCoroutine(SpawnBullets(bulletMoveVector, bulletDirection, patternData));

                angleSpread += angleStepSpread;
            }

            angle += angleStep;
        }
    }

    IEnumerator SpawnBullets(Vector3 bulletMoveVector, Vector2 bulletDirection, PatternData patternData) {
        for (int y = 0; y < patternData.BulletStackAmount; y++) {
            GameObject bulletPoolAccordingToShape = GetBulletPoolAccordingToShape(patternData.BulletData.Shape);
            GameObject bullet = bulletPoolAccordingToShape.GetComponent<ObjectPool>().GetObject();
            bullet.transform.position = bulletMoveVector;
            bullet.GetComponent<Bullet>().SetBulletData(patternData.BulletData);

            if (patternData.AimedAtPlayer) {
                if (FindObjectOfType<Player>()) {
                    bullet.GetComponent<Bullet>().SetInitialMovementDirection((FindObjectOfType<Player>().transform.position));
                }         
            }
            else {
                bullet.GetComponent<Bullet>().SetInitialMovementDirection(bulletDirection);
            }

            
            bullet.SetActive(true);
            yield return new WaitForSeconds(patternData.TimeBetweenBulletStackSpawns);
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
    public void SetPatternData(PatternData patternData) {
        this.patternData = patternData;
    }

    public void SetBulletPools(GameObject bulletPools) {
        this.bulletPools = bulletPools;
    }
    #endregion
}
