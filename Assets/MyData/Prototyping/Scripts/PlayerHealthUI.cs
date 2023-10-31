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
    }

    private void OnDisable()
    {
        healthManager.OnDamageTaken -= OnDamageTaken;
    }

    private void OnDamageTaken()
    {
        healthBarImage.fillAmount = healthManager.Health / healthManager.MaxHealth;
    }
}
