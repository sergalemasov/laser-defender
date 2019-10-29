using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player:")]
    [SerializeField] float moveSpeedX = 10f;
    [SerializeField] float moveSpeedY = 10f;
    [SerializeField] int health = 200;
    [SerializeField] float explosionDuration = 0.5f;

    [Header("Projectile:")]
    [SerializeField] GameObject laserPrefab = null;
    [SerializeField] float projectileFiringPeriod = 0.5f;
    [SerializeField] float projectileSpeed = 10f;

    [Header("VFX:")]
    [SerializeField] GameObject explosionPrefab = null;

    [Header("SFX:")]
    [SerializeField] AudioClip laserSound = null;
    [SerializeField] AudioClip explosionSound = null;
    [SerializeField] [Range(0, 1)] float soundVolume = 0.7f;

    // state
    Vector3 minCoords;
    Vector3 maxCoords;
    Vector3 selfSize;

    GameSession gameSession = null;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        SetupMoveBoundaries();
        StartCoroutine("ListenFire");
        gameSession.UpdateHealth(health);
    }

    void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        minCoords = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxCoords = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        selfSize = GetComponent<Renderer>().bounds.size;

        float selfHalfX = selfSize.x / 2;
        float selfHalfY = selfSize.y / 2;

        minCoords.x += selfHalfX;
        maxCoords.x -= selfHalfX;

        minCoords.y += selfHalfY;
        maxCoords.y -= selfHalfY;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * moveSpeedX;
        var deltaY = Input.GetAxis("Vertical") * moveSpeedY;

        deltaX *= Time.deltaTime;
        deltaY *= Time.deltaTime;

        float newPosX = Mathf.Clamp(transform.position.x + deltaX, minCoords.x, maxCoords.x);
        float newPosY = Mathf.Clamp(transform.position.y + deltaY, minCoords.y, maxCoords.y);

        transform.position = new Vector2(newPosX, newPosY);
    }

    IEnumerator ListenFire()
    {
        while(true)
        {
            yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
            yield return FireContinuously();
        }
    }

    IEnumerator FireContinuously()
    {
        while (Input.GetButton("Fire1"))
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, soundVolume / 3);


            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) {
            return;
        }

        ProcessHit(damageDealer);
    }

    void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        health = health >= 0 ? health : 0;
        damageDealer.Hit();

        gameSession.UpdateHealth(health);

        if (health == 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);

        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, soundVolume);

        Destroy(gameObject);

        Level level = FindObjectOfType<Level>();
        level.LoadGameOver();
    }
}
