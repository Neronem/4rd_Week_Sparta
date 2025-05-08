using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Collision2D collision;

    float widthPadding;

    void Start()
    {
        collision = GetComponent<Collision2D>();
    }

    void Update()
    {
        widthPadding = Mathf.Lerp(10, 3, Time.deltaTime / 100);
    }

    Vector3 RandomPosition(Vector3 lastPosition)
    {
        //y��ġ ���� 0 = ���� ��ֹ� 100 = �߰� ��ֹ� 200 = ���� ��ֹ�
        //0�� 1�� ���� 100�� 2�� ���� 200�� �����̵����� ȸ��
        //���� ��ġ
        float[] Y = { 0, 100, 200 };
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
