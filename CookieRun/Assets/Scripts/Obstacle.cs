using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Collider2D collider;

    float widthPadding = 3;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        //widthPadding = Mathf.Lerp(10, 3, Time.deltaTime / 100);
    }

    public Vector3 RandomPosition(Vector3 lastPosition)
    {
        //y위치 지정 0 = 낮은 장애물 100 = 중간 장애물 200 = 높은 장애물
        //0은 1단 점프 100은 2단 점프 200은 슬라이딩으로 회피
        //임의 수치
        float[] Y = { -2, 0, 2 };
        float posY = Y[Random.Range(0, Y.Length)];

        Vector3 place = lastPosition + new Vector3(widthPadding, 0);
        place.y = posY;

        transform.position = place;
        return place;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //콜라이더 트리거로 히트 확인
        //플레이어에 히트 판정 생성 바람
        //Player player = collision.GetComponent<Player>();
        //if (player != null)
        //{
        //    Hit
        //}
    }
}
