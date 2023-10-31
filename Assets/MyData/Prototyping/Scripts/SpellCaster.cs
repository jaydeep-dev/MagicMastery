using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private List<GameObject> spellsList;

    private void Start()
    {
        InvokeRepeating(nameof(FireBullet), 5, 5);
        InvokeRepeating(nameof(FireMissile), 1, 10);
    }

    private void FireMissile()
    {
        var enemy = FindObjectOfType<EnemyController>();
        if (enemy != null )
        {
            var dir = (enemy.transform.localPosition - transform.position).normalized * 2;
            var missile = Instantiate(spellsList[1], transform.position + dir, Quaternion.identity);
            var rb = missile.GetComponent<Rigidbody2D>();
            missile.transform.up = dir;
            rb.AddForce(dir * 5f, ForceMode2D.Impulse);
            Destroy(missile, 10f);
        }
    }

    private void FireBullet()
    {
        var enemy = FindObjectOfType<EnemyController>();

        Vector3 dir;
        if (enemy != null)
        {
            dir = (enemy.transform.localPosition - transform.position).normalized * 2;
            Debug.Log("Dir: " + dir + "Enemy: " + enemy.transform.localPosition + " Bullet: " + transform.localPosition);
        }
        else
            dir = transform.up.normalized * 2;

        var bullet = Instantiate(spellsList[0], transform.position + dir, Quaternion.identity);
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * 10f, ForceMode2D.Impulse);
        Destroy(bullet, 10f);
    }
}
