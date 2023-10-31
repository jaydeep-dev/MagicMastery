using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpCollector : MonoBehaviour
{
    [SerializeField] private int maxExp;
    [SerializeField] private Image expBar;

    private int currentExp = 0;
    private int targetExp = 0;
    private LTDescr expBarTween;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PickupItem item) && item.pickupType == PickupItem.Type.Exp)
        {
            targetExp += item.pickupQuantity;
            LeanTween.move(item.gameObject, transform, .25f)
                     .setOnComplete(() => TweenExpBar(targetExp))
                     .setDestroyOnComplete(true);
        }
    }

    private void TweenExpBar(float targetExp)
    {
        if (expBarTween != null)
            LeanTween.cancel(expBarTween.id);

        expBarTween = LeanTween.value(currentExp, targetExp, 1f)
        .setOnComplete(() => HandleExpBar(currentExp))
        .setOnUpdate((float val) => HandleExpBar(val));
    }

    private void HandleExpBar(float val)
    {
        if (val >= maxExp)
        {
            currentExp = 0;
            targetExp = ((int)expBarTween.to.x) - maxExp;
            Debug.Log(currentExp + " -> " + (expBarTween.to.x - maxExp));
            TweenExpBar(targetExp);
            maxExp *= 2; // Makling twice as long
            return;
        }
        currentExp = (int)val;
        expBar.fillAmount = val / maxExp;
    }
}
