using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    #region Player Variables
    [Header("Player Data")]
    [SerializeField] PlayerData playerData;

    [Header("Player Stats")]
    [SerializeField] float playerHealth;

    [Header("Player Bullet Pool")]
    [SerializeField] GameObject bulletPool;

    Coroutine firingCoroutine;
    #endregion

    #region Unity Callback Functions
    void Start() {
        playerHealth = playerData.GetHealth();
    }

    void Update() {
        Move();
        Fire();
    }
    #endregion

    #region Functions
    private void Move() {
        var deltaX = GetComponent<PlayerInputHandler>().normalizeInputX * playerData.GetMoveSpeed() * Time.deltaTime;
        var deltaY = GetComponent<PlayerInputHandler>().normalizeInputY * playerData.GetMoveSpeed() * Time.deltaTime;

        float xMin = GetMoveBoundaryHorizontal(Camera.main, new Vector3(0, 0, 0), playerData.GetPadding());
        float xMax = GetMoveBoundaryHorizontal(Camera.main, new Vector3(1, 0, 0), -playerData.GetPadding());

        float yMin = GetMoveBoundaryVertical(Camera.main, new Vector3(0, 0, 0), playerData.GetPadding());
        float yMax = GetMoveBoundaryVertical(Camera.main, new Vector3(0, 1, 0), -playerData.GetPadding());

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire() {
        if (GetComponent<PlayerInputHandler>().fireInput && firingCoroutine == null) {
            firingCoroutine = StartCoroutine(FireContiniuously());
        }

        if (!GetComponent<PlayerInputHandler>().fireInput && firingCoroutine != null) {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContiniuously() {
        while (true) {
            GameObject bullet = bulletPool.GetComponent<ObjectPool>().GetObject();
            bullet.transform.position = transform.Find("Gun Position").position;
            bullet.GetComponent<Bullet>().SetBulletData(playerData.GetPlayerBulletData());
            Vector3 direction = new Vector3(transform.position.x, 10, 0);
            bullet.GetComponent<Bullet>().SetInitialMovementDirection(direction);
            bullet.SetActive(true);

            yield return new WaitForSeconds(playerData.GetProjectileFiringPeriod());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (!bullet) { return; }
        ProcessHit(bullet);
    }

    private void ProcessHit(Bullet bullet) {
        playerHealth -= bullet.GetBulletData().Damage;
        bullet.DestroyThisBullet();

        if (playerHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        //FindObjectOfType<Level>().LoadGameOver();
        gameObject.SetActive(false);
        //GameObject explosion = Instantiate(deathVfxPrefab, transform.position, transform.rotation);
        //Destroy(explosion, durationOfExplosion);
        //AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position, deathSfxVolume);
    }

    #endregion

    #region Getters and Setters
    private float GetMoveBoundaryHorizontal(Camera camera, Vector3 vector, float padding) {
        return camera.ViewportToWorldPoint(vector).x + padding;
    }

    private float GetMoveBoundaryVertical(Camera camera, Vector3 vector, float padding) {
        return camera.ViewportToWorldPoint(vector).y + padding;
    }

    public float GetPlayerHealth() {
        return playerHealth;
    }
    #endregion
}
