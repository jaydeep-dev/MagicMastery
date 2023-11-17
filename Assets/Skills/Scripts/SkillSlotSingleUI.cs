using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotSingleUI : MonoBehaviour
{
    [SerializeField] private Image logo;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private List<GameObject> levelsUIVisualList;

    private Button slot;

    private void Awake()
    {
        slot = GetComponent<Button>();
    }

    public void SetSkillData(SkillUIInfo skillInfo)
    {
        logo.sprite = skillInfo.logo;
        skillName.text = skillInfo.skillName.ToString();
        for (int i = 0; i < levelsUIVisualList.Count; i++)
        {
            levelsUIVisualList[i].SetActive(i < skillInfo.currentLevel + 1);
        }
        slot.onClick.RemoveAllListeners();
        slot.onClick.AddListener(() => Time.timeScale = 1f);
        slot.onClick.AddListener(() => SkillUIController.InvokeSelectedEvent(skillInfo.skillName));
    }
}
