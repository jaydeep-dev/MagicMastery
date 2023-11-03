using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotSingleUI : MonoBehaviour
{
    [SerializeField] private Image logo;
    [SerializeField] private TextMeshProUGUI skillName;

    private Button slot;

    private void Awake()
    {
        slot = GetComponent<Button>();
    }

    public void SetSkillData(SkillUIInfo skillInfo)
    {
        this.logo.sprite = skillInfo.logo;
        this.skillName.text = skillInfo.skillName.ToString();
        slot.onClick.RemoveAllListeners();
        slot.onClick.AddListener(() => Time.timeScale = 1f);
        slot.onClick.AddListener(() => SkillUIController.InvokeSelectedEvent(skillInfo.skillName));
    }
}
