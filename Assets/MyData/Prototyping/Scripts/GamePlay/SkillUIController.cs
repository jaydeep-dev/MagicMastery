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

    public static bool IsShowingUI = false;

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
        IsShowingUI = true;
        var selectedUISkills = new List<SkillUIInfo>();
        foreach (var slot in skillSlotsList)
        {
            // Prepare UI List
            var uiSkills = new List<SkillNameTag>();
            allSkillsUIList.ForEach(x => uiSkills.Add(x.skillName));

            // Get Maxed-out Skills of player
            var maxedSkills = skillsHandler.GetCurrentSkills().FindAll(x => x.CurrentLevel == x.MaxLevel);

            // Remove them from UI List
            maxedSkills.ForEach(x => uiSkills.Remove(x.SkillNameTag));

            // Remove already selected skill
            selectedUISkills.ForEach(x => uiSkills.Remove(x.skillName));

            // Get Random Skill
            int randomIndex = Random.Range(0, uiSkills.Count);

            // Select Random Skill For UI
            var selectedSkill = allSkillsUIList.Find(x => x.skillName == uiSkills[randomIndex]);
            selectedUISkills.Add(selectedSkill);
            Debug.Log(selectedSkill + "____" + uiSkills[randomIndex]);

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
        IsShowingUI = false;
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