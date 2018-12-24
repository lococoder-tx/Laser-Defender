using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
   
    List<Transform> waypoints;
    int waypointIndex = 0;
    WaveConfig waveConfig;
    float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.GetMoveSpeed();
        transform.position = waypoints[waypointIndex].transform.position;
        waypointIndex++;
    }

    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
     if(waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var moveThisFrame = moveSpeed * Time.deltaTime;
            
            //Debug.Log("Transform current is " +transform.position);
            //Debug.Log("Transform goal is " + targetPosition);
           
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveThisFrame);
            if(transform.position == targetPosition)
                waypointIndex++;
        }
        else
            Destroy(gameObject);
    }
}
