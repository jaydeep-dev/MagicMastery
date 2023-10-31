using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;

    [SerializeField] private GameObject expDropPrefab;

    private Rigidbody2D rb;
    private PlayerMovement player;
    private HealthManager healthManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthManager = GetComponent<HealthManager>();
    }

    private void OnEnable()
    {
        healthManager.OnDie += OnDie;
    }

    private void OnDisable()
    {
        healthManager.OnDie -= OnDie;
    }

    private void OnDie()
    {
        var exp = Instantiate(expDropPrefab, transform.position, transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (player == null) return;

        var playerPos = player.transform.position;

        // To prevent pushing player by enemies (2+ rigidbodies colliding eachother and generating force)
        if(Vector2.Distance(playerPos, rb.position) <= 1f ) { return; }

        rb.position = Vector2.MoveTowards(rb.position, playerPos, moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(damage);
        }
    }
}
