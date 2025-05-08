using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BgLooper : MonoBehaviour
{
    int obstacleCount = 0; //장애물 개수 선언
    Vector3 obstacleLastPosition = Vector3.zero; //마지막 장애물 위치 선언

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); //장애물 찾아오기
        obstacleLastPosition = obstacles[0].transform.position; //마지막 장애물 위치 초기화
        obstacleCount = obstacles.Length; //장애물 개수 초기화

        for (int i = 0; i < obstacleCount; i++)
        {
            //장애물 마지막 위치 = i번째 장애물 위치
            obstacleLastPosition = obstacles[i].RandomPosition(obstacleLastPosition);
        }
    }

    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("BackGround");
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * (backgrounds.Length / 2);
            collision.transform.position = pos;
            return;
        }


        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            Debug.Log("Trigger");
            obstacleLastPosition = obstacle.RandomPosition(obstacleLastPosition);
        }
    }
}