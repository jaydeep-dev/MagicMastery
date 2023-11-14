using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIController : MonoBehaviour
{
    [SerializeField] private SkillsHandler skillsHandler;

    [Header("Skill UI Settings")]
    [SerializeField] private GameObject skillUI;
    [SerializeField] private List<SkillSlotSingleUI> skillSlotsList;

    [SerializeField] private List<SkillUIInfo> allSkillsUIList;

    public static event System.Action<SkillNameTag> OnSkillSelected;

    private void OnEnable()
    {
        ExpCollector.OnLevelUp += OnLevelUp;
        OnSkillSelected += ManageActiveSkills;
    }

    private void ManageActiveSkills(SkillNameTag selectedSkill)
    {
        var currentActiveSkills = skillsHandler.GetCurrentSkills();
        var isOwnedSkill = currentActiveSkills.Exists(x => x.SkillNameTag == selectedSkill);
        Debug.Log(isOwnedSkill);

        if(isOwnedSkill)
            skillsHandler.LevelUpSkill(selectedSkill);
        else
            skillsHandler.ActivateSkill(selectedSkill);
    }

    private void OnLevelUp()
    {
        skillUI.SetActive(true);
        Time.timeScale = 0;
        foreach (var slot in skillSlotsList)
        {
            // Get Random Skill
            int randomIndex = Random.Range(0, allSkillsUIList.Count);

            // Prepare UI List
            var skills = new List<SkillNameTag>();
            allSkillsUIList.ForEach(x => skills.Add(x.skillName));

            // Get Maxed-out Skills of player
            var currentSkills = skillsHandler.GetCurrentSkills().FindAll(x => x.CurrentLevel == x.MaxLevel);
            var maxedSkills = new List<SkillNameTag>();
            foreach (var skill in currentSkills)
            {
                if (skill.CurrentLevel == skill.MaxLevel) continue;
                maxedSkills.Add(skill.SkillNameTag);
            }

            // Remove them from UI List
            maxedSkills.ForEach(x => skills.Remove(x));

            // Select Random Skill For UI
            var selectedSkill = allSkillsUIList.Find(x => x.skillName == skills[randomIndex]);
            Debug.Log(selectedSkill + "____" + skills[randomIndex]);

            // Set Data for UI
            slot.SetSkillData(selectedSkill);
        }
    }

    private void OnDisable()
    {
        ExpCollector.OnLevelUp -= OnLevelUp;
        OnSkillSelected -= ManageActiveSkills;
    }

    public static void InvokeSelectedEvent(SkillNameTag tag)
    {
        OnSkillSelected?.Invoke(tag);
    }
}

[System.Serializable]
public struct SkillUIInfo
{
    public Sprite logo;
    public SkillNameTag skillName;

    public override string ToString() => JsonUtility.ToJson(this);
}