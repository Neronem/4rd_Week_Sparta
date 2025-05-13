using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollow : MonoBehaviour
{
    public Transform player; // 쫓을 플레이어
    public float followSpeed = 5f; // 따라가는 속도
    public float stopDistance = 1.5f; // 너무 가까우면 멈춤
    private bool isGoingtoGiveItem = false;
    
    public ItemData itemData;
    public float ItemGiveInterval = 20f;
    
    private Vector3 targetPosition;
    
    private void Start()
    {
        InvokeRepeating("GiveItem", 1f, ItemGiveInterval);
    }

    private void Update()
    {
        if (player == null) return;
        if (!isGoingtoGiveItem)
        {
            float distance = Vector3.Distance(transform.position, player.position); // 둘 사이 거리 구함

            if (distance > stopDistance) // 멈춰야 하는 거리보다 더 벌려져 있을 때만
            { // 플레이어한테 이동
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * followSpeed * Time.deltaTime;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isGoingtoGiveItem = false;
            }
        }
    }
    
    public void GiveItem()
    {
        if (player == null || itemData == null) return;
     
        isGoingtoGiveItem = true;
        targetPosition = player.transform.position + new Vector3(1.5f, 0, 0);
        
        GameObject go = Instantiate(itemData.Prefab, targetPosition, Quaternion.identity);
        
        BaseItem baseItem = go.GetComponent<BaseItem>();
        baseItem?.SetItemData(itemData);
        
        isGoingtoGiveItem = false;
    }
}
