using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : SkillActivator
{
    [SerializeField] float defenseI;
    [SerializeField] float defenseII;
    [SerializeField] float defenseIII;
    //player = how to get player

    public override void Activate()
    {
        base.Activate();
        player.SetStatMultiplier(Stat.Defense, defenseI);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
           player.SetStatMultiplier(Stat.Defense, defenseII);
        }
        if (CurrentLevel == 3)
        {
           player.SetStatMultiplier(Stat.Speed, defenseII);
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