using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;

    private HealthManager healthManager;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    private void OnEnable()
    {
        healthManager.OnDamageTaken += OnDamageTaken;
        healthManager.OnDie += OnDie;
    }

    private void OnDie()
    {
        SoundManager.Instance.PlayGameOverSFX();
        Destroy(gameObject);
        LeanTween.delayedCall(5f, () =>
        {
            var levelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
        });
    }

    private void OnDisable()
    {
        healthManager.OnDamageTaken -= OnDamageTaken;
        healthManager.OnDie -= OnDie;
    }

    private void OnDamageTaken()
    {
        healthBarImage.fillAmount = healthManager.Health / healthManager.MaxHealth;
    }
}
