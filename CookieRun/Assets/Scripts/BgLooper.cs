using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    int obstacleCount = 0;
    Vector3 obstacleLastPosition = Vector3.zero;
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].RandomPosition(obstacleLastPosition);
        }
    }

    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            Debug.Log("Trigger");
            obstacleLastPosition = obstacle.RandomPosition(obstacleLastPosition);
        }
    }
}
