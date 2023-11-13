using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : SkillActivator
{
    [SerializeField] float cooldownMultiplierLevel1;
    [SerializeField] float cooldownMultiplierLevel2;
    [SerializeField] float cooldownMultiplierLevel3;
    public override void Activate()
    {
        base.Activate();
        skillsAugmentor.cooldownMultiplier = cooldownMultiplierLevel1;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        skillsAugmentor.cooldownMultiplier = CurrentLevel == 2 ? cooldownMultiplierLevel2 : cooldownMultiplierLevel3;
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
