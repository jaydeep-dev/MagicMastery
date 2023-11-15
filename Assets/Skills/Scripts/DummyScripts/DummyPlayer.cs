using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    [SerializeField] List<SkillNameTag> skillsToActivate;
    [SerializeField] List<SkillNameTag> skillsToMaxLater;
    [SerializeField] List<SkillNameTag> skillsToActivateEvenLater;
    SkillsHandler skillsHandler;
    private void Awake()
    {
        skillsHandler = GetComponentInChildren<SkillsHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(var skillNameTag in skillsToActivate)
        {
            skillsHandler.ActivateSkill(skillNameTag);
        }

        Invoke(nameof(MaxLevel), 10);
    }

    void MaxLevel()
    {
        foreach(var skillNameTag in skillsToMaxLater)
        {
            skillsHandler.LevelUpSkill(skillNameTag);
            skillsHandler.LevelUpSkill(skillNameTag);
        }

        Invoke(nameof(ActivateSkillsLate), 10);
    }

    void ActivateSkillsLate()
    {
        foreach (var skillNameTag in skillsToActivateEvenLater)
        {
            skillsHandler.ActivateSkill(skillNameTag);
        }
    }
}
