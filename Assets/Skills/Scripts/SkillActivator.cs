using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillActivator : MonoBehaviour
{
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected SkillNameTag skillNameTag;
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected Transform vfx;
    [SerializeField] protected SkillsAugmentor skillsAugmentor;
    public SkillNameTag SkillNameTag => skillNameTag;
    public bool IsActive { get; private set; }
    public int CurrentLevel { get; private set; }
    public virtual int MaxLevel { get; } = 3;

    protected float cooldownTimeLeft;

    public virtual void Activate()
    {
        IsActive = true;
        CurrentLevel = 1;
        if(vfx != null)
        {
            vfx.gameObject.SetActive(true);
        }        
    }

    public virtual void LevelUp()
    {
        if(CurrentLevel < MaxLevel)
        {
            CurrentLevel++;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(!IsActive)
        {
            return;
        }

        cooldownTimeLeft -= Time.deltaTime;

        if(cooldownTimeLeft <= 0)
        {
            UseSkill();
            cooldownTimeLeft = skillsAugmentor.CalculateModifiedCooldown(cooldownTime);
        }

    }

    protected abstract void UseSkill();
}
