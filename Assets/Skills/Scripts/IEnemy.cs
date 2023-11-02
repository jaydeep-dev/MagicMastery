using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public void Damage(float damage);
    public void ChangeSpeed(float speedMultiplier);
}
