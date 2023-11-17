using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfficientXP : SkillActivator
{
    [SerializeField] float efficiencyI;
    [SerializeField] float efficiencyII;
    [SerializeField] float efficiencyIII;
    [SerializeField] Stat stat;

    IPlayer player;
    private void Awake()
    {
      player = transform.root.GetComponent<IPlayer>();
    }

    public override void Activate()
    {
        base.Activate();
        player.SetStatMultiplier(stat, efficiencyI);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
           player.SetStatMultiplier(stat, efficiencyII);
        }
        if (CurrentLevel == 3)
        {
           player.SetStatMultiplier(stat, efficiencyIII);
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
