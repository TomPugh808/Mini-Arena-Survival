
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public bool isDead  = false;
    public PlayerHealthBar healthBar;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealth(1f);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        
        currentHealth -= damage;
        healthBar.UpdateHealth((float)currentHealth / maxHealth);

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("Player has died!");
            gameObject.SetActive(false);
        }
    }
}
