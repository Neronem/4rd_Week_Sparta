using UnityEngine;


public class ScoreItem : BaseItem
{
    protected override void HandlePlayerCollision()
    {
        Debug.Log("아이템을 획득하였습니다.");
        Destroy(gameObject);
    }
}