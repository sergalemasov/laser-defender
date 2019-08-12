using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints = new List<Transform>();
    [SerializeField] float speed = 1f;

    int currentWaypointindex = 0;
    float step;
    // Start is called before the first frame update
    void Start()
    {
        CalculateStep();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsToWaypoint();
    }

    void CalculateStep()
    {
        step = speed * Time.deltaTime;
    }

    void MoveTowardsToWaypoint()
    {
        if (transform.position == waypoints[currentWaypointindex].position)
        {
            currentWaypointindex++;
        }

        if (currentWaypointindex == waypoints.Count)
        {
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointindex].position, step);
    }
}
