using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlast : SkillActivator
{
    [SerializeField] IceShot iceShotPrefab;
    List<IceShot> iceShots = new();
    [SerializeField] float baseDamage;
    [SerializeField] float increasedDamage;
    private void Awake()
    {
        for(int i=0;i < 2;i++)
        {
            var iceShot = Instantiate(iceShotPrefab, transform);
            iceShot.transform.localPosition = Vector3.zero;
            iceShot.InjectSkillsAugmentor(skillsAugmentor);
            iceShots.Add(iceShot);
        }
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        float damage = CurrentLevel == 3 ? increasedDamage : baseDamage;
        iceShots[0].UseIceShot(Vector2.right, damage);
        if(CurrentLevel > 1)
        {
            iceShots[1].UseIceShot(Vector2.up, damage);
        }
    }
}
