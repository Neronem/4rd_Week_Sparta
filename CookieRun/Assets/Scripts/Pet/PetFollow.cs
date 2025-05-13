using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollow : MonoBehaviour
{
    public Transform player; // 쫓을 플레이어
    public float followSpeed = 5f; // 따라가는 속도
    public float stopDistance = 1.5f; // 너무 가까우면 멈춤

    private void Update()
    {
        if (player == null) return;
        
        float distance = Vector3.Distance(transform.position, player.position); // 둘 사이 거리 구함

        if (distance > stopDistance) // 멈춰야 하는 거리보다 더 벌려져 있을 때만
        { // 플레이어한테 이동
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }
}
