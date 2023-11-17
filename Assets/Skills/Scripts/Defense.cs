using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : SkillActivator
{
    [SerializeField] float defenseI;
    [SerializeField] float defenseII;
    [SerializeField] float defenseIII;
    [SerializeField] Stat stat;
    IPlayer player;
    private void Awake()
    {
      player = transform.root.GetComponent<IPlayer>();
    }

    public override void Activate()
    {
        base.Activate();
        player.SetStatMultiplier(stat, defenseI);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
           player.SetStatMultiplier(stat, defenseII);
        }
        if (CurrentLevel == 3)
        {
           player.SetStatMultiplier(stat, defenseIII);
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