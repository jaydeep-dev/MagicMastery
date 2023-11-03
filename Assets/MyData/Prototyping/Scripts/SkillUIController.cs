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
        OnSkillSelected += skillsHandler.ActivateSkill;
    }

    private void OnLevelUp()
    {
        skillUI.SetActive(true);
        Time.timeScale = 0;
        foreach (var slot in skillSlotsList)
        {
            int randomIndex = Random.Range(0, skillsHandler.AllSkills.Keys.Count);
            var skills = new List<SkillNameTag>(skillsHandler.AllSkills.Keys);
            var selectedSkill = allSkillsUIList.Find(x => x.skillName == skills[randomIndex]);
            Debug.Log(skills[randomIndex]);
            Debug.Log(selectedSkill);
            slot.SetSkillData(selectedSkill);
        }
    }

    private void OnDisable()
    {
        ExpCollector.OnLevelUp -= OnLevelUp;
        OnSkillSelected -= skillsHandler.ActivateSkill;
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