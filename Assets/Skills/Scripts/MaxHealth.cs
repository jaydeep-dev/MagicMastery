using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealth : SkillActivator
{
    [SerializeField] float maxHealthI;
    [SerializeField] float maxHealthII;
    [SerializeField] float maxHealthIII;
    //player = how to get player

    public override void Activate()
    {
        base.Activate();
        player.SetStatMultiplier(Stat.MaxHealth, maxHealthI);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
           player.SetStatMultiplier(Stat.MaxHealth, maxHealthII);
        }
        if (CurrentLevel == 3)
        {
           player.SetStatMultiplier(Stat.MaxHealth, maxHealthIII);
        }
    }


    protected override void Update()
    {
        //Update not required.
    }

    protected override void UseSkill()
    {
        //It's a passive skill
    }
}