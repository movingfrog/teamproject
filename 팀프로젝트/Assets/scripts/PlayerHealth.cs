using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int damage;
    public int maxHealth = 100;
    private int currentHealth;
    private Vector3 respawnPosition = new Vector3(0, 0, 0);

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damaged();
        }
    }

    public void Damaged()
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Respawn();
    }

    void Respawn()
    {
        transform.position = respawnPosition;
        currentHealth = maxHealth;
    }
}
