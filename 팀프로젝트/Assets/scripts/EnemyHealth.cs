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
    public GameObject HealthBarPrefab;

    void Start()
    {
        currentHealth = maxHealth;
        if (EnemyHP != null)
        {
            EnemyHP.maxValue = maxHealth;
            EnemyHP.value = currentHealth;
        }
        ShowHealthBar();
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

    private void CreateHealthBar()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null && HealthBarPrefab != null)
        {
            GameObject healthBar = Instantiate(HealthBarPrefab, canvas.transform);
            EnemyHP = healthBar.GetComponent<Slider>();
            if (EnemyHP != null)
            {
                EnemyHP.maxValue = maxHealth;
                EnemyHP.value = currentHealth;
                EnemyHP.gameObject.SetActive(false);
            }
        }
    }

    private void ShowHealthBar()
    {
        if (EnemyHP != null)
        {
            EnemyHP.gameObject.SetActive(true);
        }
    }
}
