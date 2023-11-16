using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : SkillActivator
{
    [SerializeField] float healMultiplier;
    [SerializeField] int healTimeI;
    [SerializeField] int healTimeII;
    [SerializeField] int healTimeIII;
    int healTime;
    IPlayer player;
    private void Awake()
    {
      player = transform.root.GetComponent<IPlayer>();
    }

    public override void Activate()
    {
        base.Activate();
        healTime = healTimeI;
        player.Heal(healMultiplier);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
          healTime = healTimeII;
          player.Heal(healMultiplier);
        }
        if (CurrentLevel == 3)
        {
          healTime = healTimeIII;
          player.Heal(healMultiplier);
        }
    }
    // public void Heal(Stat stat, float multiplier){
    //   stat.MaxHealth = multiplier;

    // }


    protected override void Update()
    {
        if(!IsActive)
        {
            return;
        }

        healTime -= Time.deltaTime;

        if(healTime <= 0)
        {
            player.Heal(healMultiplier);
            healTime = healTimeI;
            
            if (CurrentLevel == 2)
            {
              healTime = healTimeII;
            }
            if (CurrentLevel == 3)
            {
              healTime = healTimeIII;
            }
        }

    }

    protected override void UseSkill()
    {
        //It's a passive skill
    }
}