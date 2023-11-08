using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarUIController : MonoBehaviour
{
    [SerializeField, Range(5f, 15f)] private float maxGameTimeInMin;
    [SerializeField] private Image progressbarImage;

    private float currentTime;
    private float maxTime;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateProgressBar), 0, 1);
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
            WinGame();
        }
    }

    private void WinGame()
    {
        FindObjectOfType<EnemySpawner>().gameObject.SetActive(false);
        foreach (var enemy in FindObjectsOfType<EnemyController>())
        {
            enemy.GetComponent<IDamagable>().TakeDamage(1000);
        }
    }
}
