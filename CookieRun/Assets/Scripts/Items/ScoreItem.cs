using UnityEngine;


public class ScoreItem : BaseItem
{
    
    protected override void HandlePlayerCollision(GameObject player = null)
    {
        GameManager.Instance.AddScore(itemData.Score); // itemData에서 지정.
        Destroy(gameObject);
    }
}