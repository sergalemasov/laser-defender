using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    int currentWaypointindex = 0;

    bool isInitialized = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInitialized)
        {
            return;
        }

        MoveTowardsToWaypoint();
    }

    void MoveTowardsToWaypoint()
    {
        if (transform.position == waveConfig.GetWaypoints()[currentWaypointindex].position)
        {
            currentWaypointindex++;
        }

        if (currentWaypointindex == waveConfig.GetWaypoints().Count)
        {
            Destroy(gameObject);

            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position, 
            waveConfig.GetWaypoints()[currentWaypointindex].position, 
            waveConfig.GetMoveSpeed() * Time.deltaTime
        );
    }

    public void Initialize(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;

        isInitialized = true;
    }
}
