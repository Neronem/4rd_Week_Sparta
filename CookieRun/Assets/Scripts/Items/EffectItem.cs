using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItem : BaseItem
{
    // 속도 아이템 하나당 한 번만 적용되도록.
    private bool isActived = false;
    protected override void HandlePlayerCollision(GameObject player)
    {
        // 아이템타입이 Heal이면 PlayerHealth의 Heal 발동하고 파괴
        if (itemData.Type == ItemType.Heal && player != null)
        {
            var pc = player.GetComponent<PlayerHealth>();
            pc?.Heal(itemData.Effect);
            Destroy(gameObject);
        }
        // 아니고 Speed이고 아이템 효과가 적용되지 않았으면
        else if(itemData.Type == ItemType.Speed && player != null && !isActived)
        {
            // 사실 pc 가져올 필요는 없긴 한데 
            var pc = player.GetComponent<PlayerMovement>();
            if (GameManager.Instance.speed < 15)
            {
                StartCoroutine(SpeedBuffCoroutine(pc, itemData.Effect, 5));
            }
            else
            {
                StartCoroutine(SpeedBuffCoroutine(pc, -itemData.Effect, 5));
            }
        }
    }


    private IEnumerator SpeedBuffCoroutine(PlayerMovement pc, float effect, float duration)
    {
        // 제한 걸고, 아이템 지속시간동안 파괴되지 않으니 사라진 것처럼 충돌과 이미지를 제거하고 속도 증감 지속시간 적용 후 감증 후 제한 해제하고 아이템 파괴 해제
        isActived = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GameManager.Instance.speed += effect;
        yield return new WaitForSeconds(duration);
        GameManager.Instance.speed -= effect;
        isActived = false;
        Destroy(gameObject);
    }
}