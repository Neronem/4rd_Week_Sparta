using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItem : BaseItem
{
    protected override void HandlePlayerCollision(GameObject player = null)
    {
        // if (itemData.Type == ItemType.Heal && player != null)
        // {
        //     var pc = player.GetComponent<PlayerController>();
        //     pc?.Heal(itemData.Effect);
        // }

        Destroy(gameObject);
    }
}