using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanShakeUIItems : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float shakePercentage;
    [SerializeField] private AnimationCurve shakeCurve;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        LeanTween.moveX(rect, rect.anchoredPosition.x * (1 - shakePercentage/100f), 1f).setEase(shakeCurve).setLoopPingPong().setDelay(Random.value);
        LeanTween.moveY(rect, rect.anchoredPosition.y * (1 - shakePercentage/100f), 1f).setEase(shakeCurve).setLoopPingPong().setDelay(Random.value);
    }
}
