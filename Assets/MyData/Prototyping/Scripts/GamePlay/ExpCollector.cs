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

    public static event Action OnLevelUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PickupItem item) && item.pickupType == PickupItem.Type.Exp)
        {
            targetExp += item.pickupQuantity;
            LeanTween.move(item.gameObject, transform, .1f)
                     .setOnComplete(() => TweenExpBar(targetExp))
                     .setDestroyOnComplete(true);
        }
    }

    private void OnDestroy()
    {
        OnLevelUp = null;
    }

    private void TweenExpBar(float targetExp)
    {
        if (expBarTween != null)
            LeanTween.cancel(expBarTween.id);

        expBarTween = LeanTween.value(currentExp, targetExp, .5f)
        .setOnComplete(() => HandleExpBar(currentExp))
        .setOnUpdate((float val) => HandleExpBar(val));
    }

    private void HandleExpBar(float val)
    {
        if (val >= maxExp)
        {
            LevelUp();
            return;
        }
        currentExp = (int)val;
        expBar.fillAmount = val / maxExp;
    }

    private void LevelUp()
    {
        currentExp = 0;
        targetExp = ((int)expBarTween.to.x) - maxExp;
        Debug.Log(currentExp + " -> " + targetExp);
        TweenExpBar(targetExp);
        maxExp *= 2; // Makling twice as long
        OnLevelUp?.Invoke();
    }
}
