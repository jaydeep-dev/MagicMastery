using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCollector : MonoBehaviour
{
    [SerializeField] private int maxExp;

    private int currentExp = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PickupItem item) && item.pickupType == PickupItem.Type.Exp)
        {
            LeanTween.value(currentExp, item.pickupQuantity, .5f).setOnComplete(HandleExpBar(item));
        }
    }

    private Action HandleExpBar(PickupItem item)
    {
        if(currentExp + item.pickupQuantity > maxExp)
        {
            maxExp *= 2; // Makling twice as long
            currentExp = 0;
        }
        return () => currentExp += item.pickupQuantity;
    }
}
