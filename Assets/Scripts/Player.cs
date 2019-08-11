using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeedX = 10f;
    [SerializeField] float moveSpeedY = 5f;
    [SerializeField] float projectileFiringPeriod = 0.5f;

    [SerializeField] GameObject laserPrefab = null;

    // state
    Vector3 minCoords;
    Vector3 maxCoords;
    Vector3 selfSize;

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
        StartCoroutine("ListenFire");
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
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
}
