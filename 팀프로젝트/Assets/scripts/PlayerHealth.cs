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
    public float knockbackForce = 2.5f; // 넉백힘
    public float knockbackDuration = 0.2f; // 넉백 시간
    private bool isKnockback = false;
    
    // Iinvincibility Time
    private bool isInvincible = false;
    public float invincibilityDuration = 0.3f; //무적 시간
    private Color currentColor; // 지금 색

    private SpriteRenderer spriteRenderer;
    private int PlayerLayer;
    private int PlayerDamagedLayer;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = spriteRenderer.color;

        PlayerLayer = LayerMask.NameToLayer("Player");
        PlayerDamagedLayer = LayerMask.NameToLayer("PlayerDamaged");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isKnockback)
        {
            Damaged();
            Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
            StartCoroutine(Knockback(knockbackDir));
            StartCoroutine(invincibility());
        }
    }

    public void Damaged()
    {
        if (currentHealth > 0)
        {
            Debug.Log("Damaged");
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Debug.Log("Die");
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

        while (timer <= knockbackDuration)
        {
            transform.position += (Vector3)(knockbackDir * knockbackForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        isKnockback = false;
    }

    private IEnumerator invincibility()
    {
        isInvincible = true;
        gameObject.layer = PlayerDamagedLayer;
        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a * 0.5f); // 반투명색으로 전환
        yield return new WaitForSeconds(invincibilityDuration);

        spriteRenderer.color = currentColor; // 원래 색으로 변환
        gameObject.layer = PlayerLayer;
        isInvincible = false;
    }
}
