using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarUIController : MonoBehaviour
{
    [SerializeField, Range(1f, 15f)] private float maxGameTimeInMin;
    [SerializeField] private Image progressbarImage;

    private float currentTime;
    private float maxTime;

    public static event System.Action OnTimeOver;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateProgressBar), 0, 1);
    }

    private void OnEnable()
    {
        EnemySpawner.OnWaveSpawned += OnWaveSpawned;
    }

    private void OnWaveSpawned(float remainingTime)
    {
        Debug.Log("Current Time: " + currentTime + " Remaining Time: " + remainingTime + " Target Time: " + (currentTime + remainingTime));
        currentTime += remainingTime;
    }

    private void OnDisable()
    {
        EnemySpawner.OnWaveSpawned -= OnWaveSpawned;
    }

    private void UpdateProgressBar()
    {
        currentTime++;
        maxTime = maxGameTimeInMin * 60f;
        float ratio = currentTime / maxTime;
        progressbarImage.fillAmount = ratio;
        Debug.Log(ratio + " : " + currentTime + " : " + maxTime);

        if (currentTime >= maxTime)
        {
            CancelInvoke();
            OnTimeOver?.Invoke();
        }
    }
}
