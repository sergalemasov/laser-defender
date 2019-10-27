using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = -10f;
    [SerializeField] float explosionDuration = 0.5f;

    [SerializeField] [Range(0, 1)] float soundVolume = 0.7f;

    [SerializeField] GameObject explosionPrefab = null;
    [SerializeField] GameObject laserPrefab = null;
    [SerializeField] AudioClip laserSound = null;
    [SerializeField] AudioClip explosionSound = null;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        RenewShotCounter();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        ProcessHit(damageDealer);
    }

    void ProcessHit(DamageDealer damageDealer)
    {
        if (!damageDealer)
        {
            return;
        }

        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Explode();
        }
    }

    void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f)
        {
            Fire();
            RenewShotCounter();
        }
    }

    void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, soundVolume);
    }

    void RenewShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);

        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, soundVolume);

        Destroy(gameObject);
    }
}
