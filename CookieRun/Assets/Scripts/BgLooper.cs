using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BgLooper : MonoBehaviour
{
    int obstacleCount = 0; //��ֹ� ���� ����
    Vector3 obstacleLastPosition = Vector3.zero; //������ ��ֹ� ��ġ ����

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); //��ֹ� ã�ƿ���
        obstacleLastPosition = obstacles[0].transform.position; //������ ��ֹ� ��ġ �ʱ�ȭ
        obstacleCount = obstacles.Length; //��ֹ� ���� �ʱ�ȭ

        for (int i = 0; i < obstacleCount; i++)
        {
            //��ֹ� ������ ��ġ = i��° ��ֹ� ��ġ
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
