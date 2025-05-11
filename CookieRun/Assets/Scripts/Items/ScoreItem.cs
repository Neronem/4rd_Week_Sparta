using UnityEngine;


public class ScoreItem : BaseItem
{
    
    protected override void HandlePlayerCollision(GameObject player)
    {
        GameManager.Instance.AddScore(itemData.Score); // itemData에서 지정.
        Destroy(gameObject); // 습득한 아이템 파괴
    }
}