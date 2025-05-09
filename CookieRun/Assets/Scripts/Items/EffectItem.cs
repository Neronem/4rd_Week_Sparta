using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItem : BaseItem
{
    protected override void HandlePlayerCollision()
    {
        
        // GameManager.Instance.AddScore(itemData.Score); // itemData에서 지정.
        
        // Debug.Log($"[ScoreItem] {itemData.Type} 획득, 점수 +{itemData.Score}");
        Destroy(gameObject);
        
        // Debug.Log("아이템을 획득하였습니다.");
        // Destroy(gameObject);
    }
}
