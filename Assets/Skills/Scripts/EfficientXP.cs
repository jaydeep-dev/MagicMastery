using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfficientXP : SkillActivator
{
    [SerializeField] float efficiencyI;
    [SerializeField] float efficiencyII;
    [SerializeField] float efficiencyIII;
    //player = how to get player

    public override void Activate()
    {
        base.Activate();
        player.SetStatMultiplier(Stat.XPGainEfficiency, efficiencyI);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
           player.SetStatMultiplier(Stat.XPGainEfficiency, efficiencyII);
        }
        if (CurrentLevel == 3)
        {
           player.SetStatMultiplier(Stat.XPGainEfficiency, efficiencyIII);
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
