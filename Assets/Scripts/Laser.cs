using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float missileSpeed = 10f;

    Rigidbody2D selfRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        selfRigidBody2D = GetComponent<Rigidbody2D>();
        SetInitialSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetInitialSpeed()
    {
        selfRigidBody2D.velocity = new Vector2(0, missileSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "MissileCollector")
        {
            Destroy(gameObject);
        }
    }
}
