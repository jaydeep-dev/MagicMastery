using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;

    [SerializeField] private GameObject expDropPrefab;

    private Rigidbody2D rb;
    private PlayerMovement player;
    private HealthManager healthManager;

    private bool canDamage = false;
    private float currentTime = 0f;
    private const float damageInterval = 1f;

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
        Instantiate(expDropPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
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
        if (player == null) 
            return;

        var playerPos = player.transform.position;

        // To prevent pushing player by enemies (2+ rigidbodies colliding eachother and generating force)
        if(Vector2.Distance(playerPos, rb.position) <= 1f )
            return;

        rb.position = Vector2.MoveTowards(rb.position, playerPos, moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        currentTime += Time.deltaTime;
        if (currentTime >= damageInterval && !canDamage)
        {
            canDamage = true;
            currentTime = 0f;
        }

        bool isPlayer = other.transform.TryGetComponent(out PlayerMovement player);
        if (isPlayer && canDamage)
        {
            var damagable = player.GetComponent<IDamagable>();
            damagable.TakeDamage(damage);
            canDamage = false;
        }
    }

    public void Damage(float damage)
    {
        healthManager.TakeDamage(damage);
    }

    public void ChangeSpeed(float speedMultiplier)
    {
        moveSpeed *= speedMultiplier;
    }
}
