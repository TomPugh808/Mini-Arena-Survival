
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float stopDistance = 1f;
    public int attackDamage = 10;
    public float attackCooldown = 1f;

    private Rigidbody2D rb;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private float nextAttackTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= stopDistance + 0.2f && Time.time >= nextAttackTime)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();

            if (health == null || health.isDead)
                return;

            if (health != null)
            {
                health.TakeDamage(attackDamage);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (Vector2)player.position - rb.position;
        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            rb.linearVelocity = direction.normalized * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (spriteRenderer != null && distance > 0.01f)
        {
            spriteRenderer.flipX = direction.x < 0;
        }
    }
}
