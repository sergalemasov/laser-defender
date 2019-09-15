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

    [SerializeField] GameObject laserPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        RenewShotCounter();
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
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Destroy(gameObject);
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
    }

    void RenewShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
}
