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
        //y위치 지정 -2 = 낮은 장애물 0 = 중간 장애물 2 = 높은 장애물
        //-2은 1단 점프 0은 2단 점프 2은 슬라이딩으로 회피
        //임의 수치
        //y위치 랜덤화
        float[] Y = { -4.0f, -2.8f, -1.8f };
        float posY = Y[Random.Range(0, Y.Length)];

        //위치 = 마지막 위치 + 가로간격
        Vector3 place = lastPosition + new Vector3(widthPadding, 0);
        place.y = posY;

        //장애물 위에서 오면 반전
        if (place.y > -2f)
        {
            transform.localScale = new Vector3(1, -1, 1);  //위에서 내려오는 모양
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);   //기본 상태
        }


        //마지막 위치로 이동
        transform.position = place;
        //위치 반환
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
