using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : SkillActivator
{
    [SerializeField] float damageMultiplierLevel1;
    [SerializeField] float damageMultiplierLevel2;
    [SerializeField] float damageMultiplierLevel3;
    public override void Activate()
    {
        base.Activate();
        skillsAugmentor.damageMultiplier = damageMultiplierLevel1;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        skillsAugmentor.damageMultiplier = CurrentLevel == 2 ? damageMultiplierLevel2 : damageMultiplierLevel3;
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
