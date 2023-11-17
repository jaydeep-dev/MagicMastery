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
    [SerializeField] private ParticleSystem deathParticlePrefab;

    public static event System.Action OnAnyEnemyKilled;
    public static event System.Action OnBossKilled;

    private Rigidbody2D rb;
    private PlayerMovement player;
    private HealthManager healthManager;

    private bool canDamage = false;
    private float currentTime = 0f;
    private const float damageInterval = 1f;

    private readonly int xMoveHash = Animator.StringToHash("xMove");
    private readonly int yMoveHash = Animator.StringToHash("yMove");
    private readonly int attackHash = Animator.StringToHash("Attack");

    private int damageTweenId;

    private Animator animator;
    private float speedMultiplier = 1;

    [field: SerializeField] public bool IsBoss { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthManager = GetComponent<HealthManager>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        healthManager.OnDamageTaken += OnDamageTaken;
        healthManager.OnDie += OnDie;
    }

    private void OnDamageTaken()
    {
        if(LeanTween.isTweening(damageTweenId))
            return;

        damageTweenId = LeanTween.color(gameObject, Color.red, .1f).setLoopPingPong(2).id;
    }

    private void OnDie()
    {
        var exp = Instantiate(expDropPrefab, transform.position, transform.rotation);

        var deathParticle = Instantiate(deathParticlePrefab, transform.position, transform.rotation);
        LeanTween.delayedCall(deathParticle.gameObject, deathParticle.main.duration, () => Destroy(deathParticle.gameObject));

        LeanTween.cancel(gameObject);

        OnAnyEnemyKilled?.Invoke();

        if (IsBoss)
            OnBossKilled?.Invoke();

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        healthManager.OnDamageTaken -= OnDamageTaken;
        healthManager.OnDie -= OnDie;
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
        var finalMovespeed = moveSpeed * speedMultiplier;
        rb.position = Vector2.MoveTowards(rb.position, playerPos, finalMovespeed * Time.fixedDeltaTime);

        var moveVector = (playerPos - (Vector3)rb.position).normalized;

        animator.SetFloat(xMoveHash, moveVector.x);
        animator.SetFloat(yMoveHash, moveVector.y);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        bool isPlayer = other.transform.TryGetComponent(out PlayerMovement player);

        if (isPlayer)
            currentTime += Time.deltaTime;

        if (currentTime >= damageInterval && !canDamage)
        {
            canDamage = true;
        }

        if (isPlayer && canDamage)
        {
            if (IsBoss)
                animator.SetTrigger(attackHash);

            var damagable = player.GetComponent<IDamagable>();
            damagable.TakeDamage(damage);
            canDamage = false;
            currentTime = 0f;
        }
    }

    public void Damage(float damage)
    {
        healthManager.TakeDamage(damage);
    }

    public void ChangeSpeed(float speedMultiplier)
    {
        this.speedMultiplier = speedMultiplier;
    }
}
