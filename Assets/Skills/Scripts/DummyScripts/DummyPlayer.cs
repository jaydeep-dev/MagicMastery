using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    SkillsHandler skillsHandler;
    private void Awake()
    {
        skillsHandler = GetComponentInChildren<SkillsHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        skillsHandler.ActivateSkill(SkillNameTag.LighteningBolt);
        skillsHandler.ActivateSkill(SkillNameTag.ToxicField);
        skillsHandler.ActivateSkill(SkillNameTag.FireMissile);
        skillsHandler.ActivateSkill(SkillNameTag.WindGust);
        Invoke(nameof(MaxLevel), 10);
    }

    void MaxLevel()
    {
        skillsHandler.LevelUpSkill(SkillNameTag.LighteningBolt);
        skillsHandler.LevelUpSkill(SkillNameTag.LighteningBolt);
        skillsHandler.LevelUpSkill(SkillNameTag.ToxicField);
        skillsHandler.LevelUpSkill(SkillNameTag.ToxicField);
        skillsHandler.LevelUpSkill(SkillNameTag.FireMissile);
        skillsHandler.LevelUpSkill(SkillNameTag.FireMissile);
        skillsHandler.LevelUpSkill(SkillNameTag.WindGust);
        skillsHandler.LevelUpSkill(SkillNameTag.WindGust);
    }
}
