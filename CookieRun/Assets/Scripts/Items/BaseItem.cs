using UnityEngine;


public abstract class BaseItem : MonoBehaviour
{
    protected Rigidbody2D Rigidbody;
    float itemSpacing = 2f;
    
    int itemCount = 0;
    Vector3 itemLastPosition = Vector3.zero;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        BaseItem[] items = GameObject.FindObjectsOfType<BaseItem>(); 
        itemLastPosition = Vector3.zero;
        itemCount = items.Length;
        
        Debug.Log(itemCount);
        Debug.Log(itemLastPosition);

        for (int i = 0; i < itemCount; i++)
        {
            //장애물 마지막 위치 = i번째 장애물 위치
            itemLastPosition = items[i].RandomCreate(itemLastPosition);
        }
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
        
        // x 마지막 위치에서 x축만큼 더하고, y는 랜덤 생성
        Vector3 placePosition = lastPosition + new Vector3(itemSpacing, 0);
        placePosition.y = randomY;
        
        transform.position = placePosition;
        return placePosition;
    }
}
