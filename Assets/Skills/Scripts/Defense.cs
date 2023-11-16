using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : SkillActivator
{
    [SerializeField] float defenseI;
    [SerializeField] float defenseII;
    [SerializeField] float defenseIII;
    IPlayer player;
    private void Awake()
    {
      player = transform.root.GetComponent<IPlayer>();
    }

    public override void Activate()
    {
        base.Activate();
        player.SetStatMultiplier(IPlayer.Stat.Defense, defenseI);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
           player.SetStatMultiplier(IPlayer.Stat.Defense, defenseII);
        }
        if (CurrentLevel == 3)
        {
           player.SetStatMultiplier(IPlayer.Stat.Defense, defenseIII);
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