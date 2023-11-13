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
        /*skillsHandler.ActivateSkill(SkillNameTag.LighteningBolt);
        skillsHandler.ActivateSkill(SkillNameTag.ToxicField);
        skillsHandler.ActivateSkill(SkillNameTag.FireMissile);
        skillsHandler.ActivateSkill(SkillNameTag.WindGust);
        skillsHandler.ActivateSkill(SkillNameTag.IceBlast);
        skillsHandler.ActivateSkill(SkillNameTag.LavaField);*/
        //skillsHandler.ActivateSkill(SkillNameTag.IceField);
        skillsHandler.ActivateSkill(SkillNameTag.ToxicField);
        skillsHandler.LevelUpSkill(SkillNameTag.ToxicField);
        skillsHandler.LevelUpSkill(SkillNameTag.ToxicField);
        skillsHandler.ActivateSkill(SkillNameTag.LavaField);
        skillsHandler.LevelUpSkill(SkillNameTag.LavaField);
        skillsHandler.LevelUpSkill(SkillNameTag.LavaField);        
        Invoke(nameof(MaxLevel), 10);
    }

    void MaxLevel()
    {
        /*skillsHandler.LevelUpSkill(SkillNameTag.LighteningBolt);
        skillsHandler.LevelUpSkill(SkillNameTag.LighteningBolt);
        skillsHandler.LevelUpSkill(SkillNameTag.ToxicField);
        skillsHandler.LevelUpSkill(SkillNameTag.ToxicField);
        skillsHandler.LevelUpSkill(SkillNameTag.FireMissile);
        skillsHandler.LevelUpSkill(SkillNameTag.FireMissile);
        skillsHandler.LevelUpSkill(SkillNameTag.WindGust);
        skillsHandler.LevelUpSkill(SkillNameTag.WindGust);
        skillsHandler.LevelUpSkill(SkillNameTag.IceBlast);
        skillsHandler.LevelUpSkill(SkillNameTag.IceBlast);
        skillsHandler.LevelUpSkill(SkillNameTag.LavaField);
        skillsHandler.LevelUpSkill(SkillNameTag.LavaField);*/
        //skillsHandler.LevelUpSkill(SkillNameTag.IceField);
        //skillsHandler.LevelUpSkill(SkillNameTag.IceField);
        skillsHandler.ActivateSkill(SkillNameTag.Cooldown);
    }
}
