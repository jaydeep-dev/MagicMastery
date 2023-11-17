using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundsController : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent(out IEnemy enemy))
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, Vector3.zero, Time.deltaTime);
        }
    }
}
