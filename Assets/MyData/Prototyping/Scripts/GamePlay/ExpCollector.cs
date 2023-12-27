using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpCollector : MonoBehaviour
{
    [SerializeField] private float maxExp;
    [SerializeField] private Image expBar;

    private float expMultiplier = 1f;
    private float currentExp = 0;
    private float targetExp = 0;
    private LTDescr expBarTween;

    public static event Action OnLevelUp;

    private void Awake()
    {
        if(GameManager.IsGodMode)
        {
            expBar.transform.parent.gameObject.SetActive(false);
            enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PickupItem item) && item.pickupType == PickupItem.Type.Exp)
        {
            targetExp += Mathf.CeilToInt(item.pickupQuantity * expMultiplier);
            LeanTween.move(item.gameObject, transform, .1f)
                     .setOnComplete(() => TweenExpBar(targetExp))
                     .setDestroyOnComplete(true);
        }
    }

    private void OnDisable()
    {
        if (expBarTween != null)
            LeanTween.cancel(expBarTween.id);
        OnLevelUp = null;
        Debug.Log("Expcolletor Disabled");
    }

    public void SetExpMultiplier(float multiplier) => expMultiplier = multiplier;

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
        targetExp = expBarTween.to.x - maxExp;
        Debug.Log(currentExp + " -> " + targetExp);
        maxExp *= 1.75f; // FORMULA OF EASE IN QUAD

        TweenExpBar(targetExp);

        OnLevelUp?.Invoke();
    }
}
