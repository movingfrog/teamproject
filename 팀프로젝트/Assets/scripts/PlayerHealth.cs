using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Health
    public int damage;
    public int maxHealth;
    public float currentHealth;
    private Vector3 respawnPosition = new Vector3(0, 0, 0);

    // Knockback
    public float knockbackForce = 2.5f;
    public float knockbackDuration = 0.2f;
    private bool isKnockback = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isKnockback)
        {
            Damaged();
            Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
            StartCoroutine(Knockback(knockbackDir));
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

    public void Respawn()
    {
        transform.position = respawnPosition;
        currentHealth = maxHealth;
    }

    private IEnumerator Knockback(Vector2 knockbackDir)
    {
        isKnockback = true;
        float timer = 0f;

        while (timer < knockbackDuration)
        {
            transform.position += (Vector3)(knockbackDir * knockbackForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        isKnockback = false;
    }
}
