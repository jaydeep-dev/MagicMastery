using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealth : SkillActivator
{
    [SerializeField] float maxHealthI;
    [SerializeField] float maxHealthII;
    [SerializeField] float maxHealthIII;
    [SerializeField] Stat stat;
    IPlayer player;
    private void Awake()
    {
      player = transform.root.GetComponent<IPlayer>();
    }

    public override void Activate()
    {
        base.Activate();
        player.SetStatMultiplier(stat, maxHealthI);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 2)
        {
           player.SetStatMultiplier(stat, maxHealthII);
        }
        if (CurrentLevel == 3)
        {
           player.SetStatMultiplier(stat, maxHealthIII);
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