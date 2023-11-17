using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : SkillActivator
{
    [SerializeField] float speedI;
    [SerializeField] float speedII;
    [SerializeField] float speedIII;
    [SerializeField] Stat stat;
    IPlayer player;
    private void Awake()
    {
      player = transform.root.GetComponent<IPlayer>();
    }

    public override void Activate()
    {
        base.Activate();
        player.SetStatMultiplier(stat, speedI);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
           player.SetStatMultiplier(stat, speedII);
        }
        if (CurrentLevel == 3)
        {
           player.SetStatMultiplier(stat, speedIII);
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
