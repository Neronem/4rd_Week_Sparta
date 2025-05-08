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
        //y��ġ ���� -2 = ���� ��ֹ� 0 = �߰� ��ֹ� 2 = ���� ��ֹ�
        //-2�� 1�� ���� 0�� 2�� ���� 2�� �����̵����� ȸ��
        //���� ��ġ
        //y��ġ ����ȭ
        float[] Y = { -4.0f, -2.8f, -1.8f };
        float posY = Y[Random.Range(0, Y.Length)];

        //��ġ = ������ ��ġ + ���ΰ���
        Vector3 place = lastPosition + new Vector3(widthPadding, 0);
        place.y = posY;

        //��ֹ� ������ ���� ����
        if (place.y > -2f)
        {
            transform.localScale = new Vector3(1, -1, 1);  //������ �������� ���
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);   //�⺻ ����
        }


        //������ ��ġ�� �̵�
        transform.position = place;
        //��ġ ��ȯ
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
