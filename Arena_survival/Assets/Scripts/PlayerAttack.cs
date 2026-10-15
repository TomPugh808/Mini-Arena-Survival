
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 1.5f;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange
        );

        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
