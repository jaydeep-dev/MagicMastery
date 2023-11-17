using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : SkillActivator
{
    [SerializeField] float healMultiplier;
    [SerializeField] int healTimeI;
    [SerializeField] int healTimeII;
    [SerializeField] int healTimeIII;
    IPlayer player;
    private void Awake()
    {
      player = transform.root.GetComponent<IPlayer>();
    }

    public override void Activate()
    {
        base.Activate();
        cooldownTime = healTimeI;
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

    }

    protected override void UseSkill()
    {
        // may need to check that player is not being damaged
        player.Heal(healMultiplier);
    }
}