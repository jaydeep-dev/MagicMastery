using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : SkillActivator
{
    [SerializeField] float healMultiplier;
    [SerializeField] int healTimeI;
    [SerializeField] int healTimeII;
    [SerializeField] int healTimeIII;
    [SerializeField] float healingStartTime;
    IPlayer player;
    float timeAccumulator;
    bool canHeal;
    private void Awake()
    {
      player = transform.root.GetComponent<IPlayer>();
    }

    public override void Activate()
    {
        base.Activate();
        cooldownTime = healTimeI;
        timeAccumulator = 0;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
          cooldownTime = healTimeII;
        }
        if (CurrentLevel == 3)
        {
          cooldownTime = healTimeIII;
        }
    }


    protected override void Update()
    {
        base.Update();        
        if(player.WasDamagedThisFrame)
        {
            timeAccumulator = 0;
            canHeal = false;
            return;
        }
        timeAccumulator += Time.deltaTime;
        if (timeAccumulator > healingStartTime && !canHeal)
        {
            canHeal = true;
        }
    }

    protected override void UseSkill()
    {
        if(!canHeal)
        {
            return;
        }

        player.Heal(healMultiplier);
    }
}