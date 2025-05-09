using System;
using System.Collections;
using UnityEngine;

// 이거 해놓으면 쓸까??
public enum ItemType
{
    Bronze,
    Silver,
    Gold,
    Heal,
    SpeedUp,
    SpeedDown,
}
[System.Serializable]
public class ItemData
{
    [SerializeField] private ItemType type;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int weight;
    [SerializeField] private int score;
    [SerializeField] private int effect;

    public ItemType Type => type; // 생성할 아이템 enum
    public GameObject Prefab => prefab; //생성할 프리펩
    public int Weight => weight; // 확률
    public int Score => score; // 점수
    public int Effect => effect; // 체력 or 속도 효과
    
}
public abstract class BaseItem : MonoBehaviour
{
    protected Collider2D Collirder;

    protected ItemData itemData;
    
    //item 별 x축 거리
    float itemSpacing = 1.5f;

    private bool isAdjusting = false;
    
    protected virtual void Awake()
    {
        Collirder = GetComponent<Collider2D>();
    }
    
    // Player랑 충돌 시 처리는 각 아이템별로.
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerCollision();  // 자식이 override 가능
            
            CreateItem createItem = FindObjectOfType<CreateItem>();
            createItem?.SpawnAndCreateItem(transform.position); 
        }
        else if (collision.gameObject.CompareTag("BgLooper"))
        {
            // BgLooper에 닿았으면 자기 자신 제거하고 새 아이템 생성
            CreateItem createItem = FindObjectOfType<CreateItem>();
            createItem?.SpawnAndCreateItem(transform.position);

            Destroy(gameObject);
        }
    }
    
    protected virtual void HandlePlayerCollision()
    {
        
    }
    
 // 아이템 장애물 라인에 따라 생성하도록
    public Vector3 RandomCreate(Vector3 lastPosition)
    {
        // -3f = 기본위치. (바닥라인)
        float randomY = -3.5f;
        
        // x 마지막 위치에서 x축만큼 더하고, y는 바닥라인에서 생성.
        Vector3 placePosition = lastPosition + new Vector3(itemSpacing, 0);
        placePosition.y = randomY;
        transform.position = placePosition;
        
        Collider2D hit = Physics2D.OverlapCircle(placePosition, 0.4f, LayerMask.GetMask("Obstacle"));
        if (hit != null)
        {
            Debug.Log("이거 작동 안되고 있냐");
            
            // 충돌 시 y 위치 변경 (ex. 위로 띄우기)
            placePosition.y = -1f;
            transform.position = placePosition;
        }
        
        //다음 item position을 위해 해당 positon 반환
        return placePosition;
    }

    // 게임 시작 ~ 종료까지, 아이템(해당 스크립트)과 장애물(Layer)이 충돌하는지 계속해서 추적하다 충돌하면 y값 이동 
    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     // 충돌한 오브젝트의 레이어가 "Obstacle"인지 확인
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
    //     {
    //         // 디버그로그가 꽤 많이 뜬다 = 움직일 때도 실행된다. = 성능에 부담이 간다. 일단 놔두고, 나중에 추가 작업이 될 지 모르겠네.
    //         // Debug.Log("장애물과 충돌 중! 아이템 위치 조정");
    //         Vector3 newPosition = transform.position;
    //         newPosition.y = -1f;
    //         transform.position = newPosition;
    //     }
    // }
    
    public void SetItemData(ItemData data)
    {
        itemData = data;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") && !isAdjusting)
        {
            StartCoroutine(AdjustYPositionStepByStep());
        }
        
    }
    
    
    IEnumerator AdjustYPositionStepByStep()
    {
        isAdjusting = true;
    
        float[] yLevels = { -2f, -1f }; // 이동하려는 y 단계들
        foreach (float targetY in yLevels)
        {
            // 위치 이동
            Vector3 newPosition = transform.position;
            newPosition.y = targetY;
            transform.position = newPosition;
    
            // FixedUpdate 후 충돌 확인
            yield return new WaitForFixedUpdate();
    
            Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.4f, LayerMask.GetMask("Obstacle"));
            if (hit == null)
            {
                break; // 충돌이 없으면 멈춤
            }
        }
    
        isAdjusting = false;
    }
}
