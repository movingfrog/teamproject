using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth;
    public Vector3 healthBarOffset = new Vector3(0, 1f, 0);
    [SerializeField] private Slider EnemyHP;

    void Start()
    {
        currentHealth = maxHealth;
        if (EnemyHP != null)
        {
            EnemyHP.maxValue = maxHealth;
            EnemyHP.value = currentHealth;
        }
    }

    private void Update()
    {
        if (EnemyHP != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
            EnemyHP.transform.position = screenPos;
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        if (EnemyHP != null)
        {
                EnemyHP.value = currentHealth;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        if (EnemyHP != null)
        {
            Destroy(EnemyHP.gameObject);
        }
    }
}
