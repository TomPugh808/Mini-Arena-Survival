
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float stopDistance = 1f;

    private Rigidbody2D rb;
    private Transform player;
    private SpriteRenderer spriteRenderer;

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
