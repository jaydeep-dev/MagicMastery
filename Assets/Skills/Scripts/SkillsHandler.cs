using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillsHandler : MonoBehaviour
{
    List<SkillNameTag> currentSkills = new();

    public Dictionary<SkillNameTag, SkillActivator> AllSkills { get; private set; } = new();
    Dictionary<SkillNameTag, SkillActivator> breakthroughSkills = new();
    public static event Action<SkillNameTag> skillBreakthrough;
    public static List<SkillNameTag> passiveSkills = new()
    {
        SkillNameTag.Heal, SkillNameTag.Attack, SkillNameTag.Defense, SkillNameTag.Health,
        SkillNameTag.Cooldown, SkillNameTag.OneOnOne, SkillNameTag.EfficientXP, SkillNameTag.Speed
    };
    private void Awake()
    {
        SkillActivator[] skillActivators = GetComponentsInChildren<SkillActivator>();
        foreach (var skillActivator in skillActivators)
        {
            if (skillActivator.SkillNameTag == SkillNameTag.GreatFireBall ||
                skillActivator.SkillNameTag == SkillNameTag.BurningPoison ||
                skillActivator.SkillNameTag == SkillNameTag.IcicleBlast ||
                skillActivator.SkillNameTag == SkillNameTag.LighteningArrow ||
                skillActivator.SkillNameTag == SkillNameTag.HellSummon)
            {
                breakthroughSkills.Add(skillActivator.SkillNameTag, skillActivator);
                continue;
            }

            AllSkills.Add(skillActivator.SkillNameTag, skillActivator);
            Debug.Log(skillActivator.name);
        }
    }

    public void ActivateSkill(SkillNameTag skillNameTag)
    {
        if (!AllSkills.TryGetValue(skillNameTag, out SkillActivator skillActivator))
        {
            return;
        }

        if (currentSkills.Contains(skillNameTag))
        {
            Debug.LogError("Already activated this skill: " + skillNameTag);
            return;
        }

        skillActivator.Activate();
        currentSkills.Add(skillNameTag);
        CheckForBreakthroughSkills();
    }

    public void LevelUpSkill(SkillNameTag skillNameTag)
    {
        if (!AllSkills.TryGetValue(skillNameTag, out SkillActivator skillActivator))
        {
            return;
        }

        if (!currentSkills.Contains(skillNameTag))
        {
            Debug.LogError("Skill hasn't activated yet: " + skillNameTag);
            return;
        }

        skillActivator.LevelUp();
        CheckForBreakthroughSkills();
    }

    public List<SkillLevelInfo> GetCurrentSkills()
    {
        List<SkillLevelInfo> currentSkillsInfo = new();
        foreach (var skillNameTag in currentSkills)
        {
            var skill = AllSkills[skillNameTag];
            currentSkillsInfo.Add(new SkillLevelInfo(skillNameTag, skill.CurrentLevel, skill.MaxLevel, false));
        }

        return currentSkillsInfo;
    }

    void CheckForBreakthroughSkills()
    {
        var currentSkillsInfos = GetCurrentSkills();
        //GreatFireBall
        if (CheckBreakthroughRecipe(currentSkillsInfos, SkillNameTag.FireMissile, SkillNameTag.WindGust, SkillNameTag.Defense, SkillNameTag.Heal))
        {
            PerformBreakthrough(SkillNameTag.FireMissile, SkillNameTag.WindGust, SkillNameTag.GreatFireBall);
            return;
        }
        //BurningPoison
        if (CheckBreakthroughRecipe(currentSkillsInfos, SkillNameTag.ToxicField, SkillNameTag.LavaField, SkillNameTag.Cooldown, SkillNameTag.EfficientXP))
        {
            PerformBreakthrough(SkillNameTag.ToxicField, SkillNameTag.LavaField, SkillNameTag.BurningPoison);
            return;
        }
        //IcicleBlast
        if (CheckBreakthroughRecipe(currentSkillsInfos, SkillNameTag.IceBlast, SkillNameTag.IceField, SkillNameTag.Speed, SkillNameTag.Health))
        {
            PerformBreakthrough(SkillNameTag.IceBlast, SkillNameTag.IceField, SkillNameTag.IcicleBlast);
            return;
        }
        //LighteningArrow
        if (CheckBreakthroughRecipe(currentSkillsInfos, SkillNameTag.DimensionalArrow, SkillNameTag.LighteningBolt, SkillNameTag.Attack))
        {
            PerformBreakthrough(SkillNameTag.DimensionalArrow, SkillNameTag.LighteningBolt, SkillNameTag.LighteningArrow);
            return;
        }
        //HellSummon
        if (CheckBreakthroughRecipe(currentSkillsInfos, SkillNameTag.FlameSummon, SkillNameTag.MeteorSummon, SkillNameTag.OneOnOne))
        {
            PerformBreakthrough(SkillNameTag.FlameSummon, SkillNameTag.MeteorSummon, SkillNameTag.HellSummon);
            return;
        }
    }

    void PerformBreakthrough(SkillNameTag activeSkill1, SkillNameTag activeSkill2, SkillNameTag breakthorughSkill)
    {
        Destroy(AllSkills[activeSkill1].gameObject);
        Destroy(AllSkills[activeSkill2].gameObject);

        AllSkills.Remove(activeSkill1);
        AllSkills.Remove(activeSkill2);

        currentSkills.Remove(activeSkill1);
        currentSkills.Remove(activeSkill2);

        AllSkills.Add(breakthorughSkill, breakthroughSkills[breakthorughSkill]);
        ActivateSkill(breakthorughSkill);

        skillBreakthrough?.Invoke(breakthorughSkill);
    }

    //can be optimized. too lazy.
    bool CheckBreakthroughRecipe(List<SkillLevelInfo> currentSkills, SkillNameTag activeSkill1, SkillNameTag activeSkill2, SkillNameTag passiveSkillOption1, SkillNameTag? passiveSkillOption2 = null)
    {
        bool activeSkill1Max = false;
        bool activeSkill2Max = false;
        bool hasPassiveSkill = false;
        foreach (var skillInfo in currentSkills)
        {
            if (skillInfo.SkillNameTag == activeSkill1 && skillInfo.CurrentLevel == skillInfo.MaxLevel)
            {
                activeSkill1Max = true;
            }
            if (skillInfo.SkillNameTag == activeSkill2 && skillInfo.CurrentLevel == skillInfo.MaxLevel)
            {
                activeSkill2Max = true;
            }
            if ((skillInfo.SkillNameTag == passiveSkillOption1) || (passiveSkillOption2 != null && skillInfo.SkillNameTag == passiveSkillOption2))
            {
                hasPassiveSkill = true;
            }
        }

        return activeSkill1Max && activeSkill2Max && hasPassiveSkill;
    }
}

public struct SkillLevelInfo
{
    public SkillNameTag SkillNameTag { get; }
    public int CurrentLevel { get; }
    public int MaxLevel { get; }
    public bool IsPassive { get; }

    public SkillLevelInfo(SkillNameTag skillNameTag, int currentLevel, int maxLevel, bool isPassive)
    {
        SkillNameTag = skillNameTag;
        CurrentLevel = currentLevel;
        MaxLevel = maxLevel;
        IsPassive = isPassive;
    }

    public override string ToString() => JsonUtility.ToJson(this);
}
