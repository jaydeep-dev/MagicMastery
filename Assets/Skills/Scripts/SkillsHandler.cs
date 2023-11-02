using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsHandler : MonoBehaviour
{
    Dictionary<SkillNameTag, SkillActivator> activeSkills = new();
    List<SkillNameTag> currentSkills = new();
    private void Awake()
    {
        SkillActivator[] skillActivators = GetComponentsInChildren<SkillActivator>();
        foreach(var skillActivator in skillActivators)
        {
            activeSkills.Add(skillActivator.SkillNameTag, skillActivator);
        }
    }

    public void ActivateSkill(SkillNameTag skillNameTag)
    {
        if(!activeSkills.TryGetValue(skillNameTag, out SkillActivator skillActivator))
        {
            return;
        }

        skillActivator.Activate();
        currentSkills.Add(skillNameTag);
    }

    public void LevelUpSkill(SkillNameTag skillNameTag)
    {
        if (!activeSkills.TryGetValue(skillNameTag, out SkillActivator skillActivator))
        {
            return;
        }

        skillActivator.LevelUp();
    }

    public List<SkillLevelInfo> GetCurrentSkills()
    {
        List<SkillLevelInfo> currentSkillsInfo = new();
        foreach(var skillNameTag in currentSkills)
        {
            var skill = activeSkills[skillNameTag];
            currentSkillsInfo.Add(new SkillLevelInfo(skillNameTag, skill.CurrentLevel, skill.MaxLevel, false));
        }

        return currentSkillsInfo;
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
}
