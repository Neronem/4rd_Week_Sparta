using UnityEngine;

// 이거 해놓으면 쓸까??
// public enum ItemType
// {
//     Bronze,
//     Silver,
//     Gold,
//     Heal
// }
[System.Serializable]
public class ItemData
{
    // public ItemType type; // 생성할 아이템 enum
    public GameObject prefab; //생성할 프리펩
    public int weight; // 확률
}
public abstract class BaseItem : MonoBehaviour
{
    protected Rigidbody2D Rigidbody;
    
    //item 별 x축 거리
    float itemSpacing = 1.5f;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // BaseItem[] items = GameObject.FindObjectsOfType<BaseItem>();
        // itemLastPosition = Vector3.zero;
        // itemCount = items.Length;
        //
        // for (int i = 0; i < itemCount; i++)
        // {
        //     //장애물 마지막 위치 = i번째 장애물 위치
        //     itemLastPosition = items[i].RandomCreate(itemLastPosition);
        // }
    }

    // Player랑 충돌 시 처리는 각 아이템별로.
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    
    // 아이템 생성은 공유. 아이템 획득 시 처리는 각자
    public Vector3 RandomCreate(Vector3 lastPosition)
    {
        // -2f = 지면, 2f = 최대 점프높이라 가정 후 랜덤 생성 위치
        float randomY = Random.Range(-3f, -2f);
        
        // x 마지막 위치에서 x축만큼 더하고, y는 랜덤 생성해서 해당 item position 확정.
        Vector3 placePosition = lastPosition + new Vector3(itemSpacing, 0);
        placePosition.y = randomY;
        transform.position = placePosition;
        
        //다음 item position을 위해 해당 positon 반환
        return placePosition;
    }
}
