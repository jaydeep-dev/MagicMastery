using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaField : SkillActivator
{
    [SerializeField] LavaFieldInstance lavaFieldInstancePrefab;
    [SerializeField] float baseDamage;
    [SerializeField] float increasedDamage;
    [SerializeField] float baseRadius;
    [SerializeField] float increasedRadius;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        var instance = Instantiate(lavaFieldInstancePrefab, transform.position, Quaternion.identity);
        instance.SetDamage(CurrentLevel == 1 ? baseDamage : increasedDamage);
        instance.SetRadius(CurrentLevel == 3 ? increasedRadius : baseRadius);
        instance.InjectSkillsAugmentor(skillsAugmentor);
        instance.gameObject.SetActive(true);
    }
}
