using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

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

    private int xMoveHash = Animator.StringToHash("xMove");
    private int yMoveHash = Animator.StringToHash("yMove");
    private int isWalkingHash = Animator.StringToHash("IsWalking");

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthManager = GetComponent<HealthManager>();
        animator = GetComponent<Animator>();
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
        LeanTween.rotateZ(exp, (Random.Range(0f, 1f) >= .5f ? -360 : 360) * 2, 1).setLoopClamp();
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

        var moveVector = (playerPos - (Vector3)rb.position).normalized;

        animator.SetBool(isWalkingHash, moveVector != Vector3.zero);
        animator.SetFloat(xMoveHash, moveVector.x);
        animator.SetFloat(yMoveHash, moveVector.y);
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
