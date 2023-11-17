using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleBlast : SkillActivator
{
    [SerializeField] IceShot iceShotPrefab;
    List<IceShot> iceShots = new();
    List<Vector2> directions = new()
    {
        Vector2.right, Vector2.left, Vector2.up, Vector2.down, 
        new Vector2(1, 1).normalized, new Vector2(1, -1).normalized,
        new Vector2(-1, 1).normalized, new Vector2(-1, -1).normalized
    };
    [SerializeField] float damage;
    private void Awake()
    {
        for (int i = 0; i < 8; i++)
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
        for(int i=0;i<iceShots.Count;i++)
        {
            iceShots[i].UseIceShot(directions[i], damage);
        }
    }
}
