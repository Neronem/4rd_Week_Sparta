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
        //y��ġ ���� 0 = ���� ��ֹ� 100 = �߰� ��ֹ� 200 = ���� ��ֹ�
        //0�� 1�� ���� 100�� 2�� ���� 200�� �����̵����� ȸ��
        //���� ��ġ
        float[] Y = { -2, 0, 2 };
        float posY = Y[Random.Range(0, Y.Length)];

        Vector3 place = lastPosition + new Vector3(widthPadding, 0);
        place.y = posY;

        transform.position = place;
        return place;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ݶ��̴� Ʈ���ŷ� ��Ʈ Ȯ��
        //�÷��̾ ��Ʈ ���� ���� �ٶ�
        //Player player = collision.GetComponent<Player>();
        //if (player != null)
        //{
        //    Hit
        //}
    }
}
