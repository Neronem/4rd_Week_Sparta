using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetGiveItem : MonoBehaviour
{
    public ItemData itemData; // 생성할 아이템 데이터
    public GameObject player;
    public float moveSpeed = 3f;
    
    
    public void GiveItem()
    {
        if (player == null || itemData == null) return;
        
        Vector3 spawnPosition = player.transform.position + new Vector3(1.5f, 0, 0);
        
        GameObject go = Instantiate(itemData.Prefab, spawnPosition, Quaternion.identity);
        
        BaseItem baseItem = go.GetComponent<BaseItem>();
        baseItem?.SetItemData(itemData);
    }
}
