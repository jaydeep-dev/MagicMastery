using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour, IPlayer
{
    public bool WasDamagedThisFrame { get; private set; }

    private PlayerMovement playerMovement;
    private ExpCollector expCollector;
    private HealthManager healthManager;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        playerMovement = GetComponent<PlayerMovement>();
        expCollector = GetComponentInChildren<ExpCollector>();
    }

    public void OnEnable()
    {
        healthManager.OnDamageTaken += OnDamageTaken;
    }

    private void OnDamageTaken()
    {
        WasDamagedThisFrame = true;
    }

    private void OnDisable()
    {
        healthManager.OnDamageTaken -= OnDamageTaken;
    }

    public void Heal(float multiplier)
    {
        healthManager.SetCurrentHealth(multiplier);
    }

    public void SetStatMultiplier(Stat stat, float multiplier)
    {
        switch (stat)
        {
            case Stat.Speed:
                playerMovement.SetMoveSpeedMultiplier(multiplier);
                break;

            case Stat.Defense:
                healthManager.SetDefenceMultiplier(multiplier);
                break;

            case Stat.XPGainEfficiency:
                expCollector.SetExpMultiplier(multiplier);
                break;

            case Stat.MaxHealth:
                healthManager.SetMaxHealth(multiplier);
                break;

            default:
                Debug.LogError("Stat :" + stat + " Is not implemented");
                break;
        }
    }
}
