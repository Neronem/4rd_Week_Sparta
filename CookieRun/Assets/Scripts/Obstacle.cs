using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Collision2D collision;

    float spacing = 0;

    void Start()
    {
        collision = GetComponent<Collision2D>();
    }

    void Update()
    {
        
    }

    //Vector3 RandomPosition(Vector3 lastPosition)
    //{
    //    float[] Y = { 0, 100, 200 };
    //    float posY = Y[Random.Range(0, Y.Length)];

    //    float posX = lastPosition.x - spacing;


    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Hit
    }
}
