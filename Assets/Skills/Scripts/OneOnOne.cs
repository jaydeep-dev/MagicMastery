using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOnOne : SkillActivator
{
    [SerializeField] float damageMultiplierForBossLevel1;
    [SerializeField] float damageMultiplierForBossLevel2;
    [SerializeField] float damageMultiplierForBossLevel3;
    public override void Activate()
    {
        base.Activate();
        skillsAugmentor.damageMultiplier = damageMultiplierForBossLevel1;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        skillsAugmentor.damageMultiplier = CurrentLevel == 2 ? damageMultiplierForBossLevel2 : damageMultiplierForBossLevel3;
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
