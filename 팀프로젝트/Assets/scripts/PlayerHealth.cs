using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    ExplosionEnemy ExplosionEnemy;
    PlayerMove PlayerMove;

    // Health
    public int damage;
    public int maxHealth;
    public float currentHealth;
    private Vector3 respawnPosition = new Vector3(0, 0, 0);
    public bool isDie = false;
    
    // Knockback
    public float knockbackForce = 2.5f; // �˹���
    public float knockbackDuration = 0.2f; // �˹� �ð�
    private bool isKnockback = false;
    
    // Iinvincibility Time
    public  bool isInvincible = false;
    public float invincibilityDuration = 0.2f; //���� �ð�
    private Color currentColor; // ���� ��

    private SpriteRenderer spriteRenderer;
    private int Player;
    private int PlayerDamaged;

    // UI
    public Slider healthUI;
    public Image DieUI;
    public Button respawnBtn;


    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = spriteRenderer.color;
        
        Player = LayerMask.NameToLayer("Player");
        PlayerDamaged = LayerMask.NameToLayer("PlayerDamaged");

        // ü�¹� �ʱ�ȭ
        if (healthUI != null )
        {
            healthUI.maxValue = maxHealth;
            healthUI.value = currentHealth;
        }

        DieUI.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isKnockback && !isInvincible)
        {
            Damaged();
            Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
            StartCoroutine(Knockback(knockbackDir));
            StartCoroutine(Invincibility());
        }
    }

    // �ǰ�
    public void Damaged()
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            Debug.Log("Damaged");
            currentHealth -= damage; ;
            if (healthUI != null)
            {
                healthUI.value = currentHealth;
            }
            if (currentHealth <= 0)
            {
                Debug.Log("Die");
                Die();
            }
            StartCoroutine(Invincibility());
            GameManager.instance.stop = true;
        }
    }

    // ����
    public void Die()
    {
        // �ð� ����
        Time.timeScale = 0f;
        isDie = true;
        Debug.Log(isDie);
        DieUI.gameObject.SetActive(true);
    }

    

    // ������
    public void Respawn()
    {
        // �ð� �帧
        Time.timeScale = 1f;
        isDie = false;
        Debug.Log(isDie);
        DieUI.gameObject.SetActive(false);

        transform.position = respawnPosition;
        currentHealth = maxHealth;
        if (healthUI != null)
        {
            healthUI.value = currentHealth;
        }
    }

    // �˹�
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

    // ���� �ð�
    public IEnumerator Invincibility()
    {
        isInvincible = true;
        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a * 0.5f); // ����������� ��ȯ
        yield return new WaitForSeconds(invincibilityDuration);

        spriteRenderer.color = currentColor; // ���� ������ ��ȯ
        isInvincible = false;

        // ���� �ð��� ������ stop�� false�� ����
        GameManager.instance.stop = false;
    }
}