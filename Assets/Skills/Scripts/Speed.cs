using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : SkillActivator
{
    [SerializeField] float speedI;
    [SerializeField] float speedII;
    [SerializeField] float speedIII;
    //player = how to get player

    public override void Activate()
    {
        base.Activate();
        player.SetStatMultiplier(Stat.Speed, speedI);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
           player.SetStatMultiplier(Stat.Speed, speedII);
        }
        if (CurrentLevel == 3)
        {
           player.SetStatMultiplier(Stat.Speed, speedII);
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