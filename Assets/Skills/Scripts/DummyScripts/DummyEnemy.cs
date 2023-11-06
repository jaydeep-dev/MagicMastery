using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public void ChangeSpeed(float speedMultiplier)
    {
        Debug.Log("Speed Changed " + speedMultiplier);
    }

    public void Damage(float damage)
    {
        Debug.Log("Damaged");
    }
}
