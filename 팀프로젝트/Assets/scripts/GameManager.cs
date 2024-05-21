using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Vector3 respawnPosion = new Vector3(0, 0, 0);

    void Start()
    {
        currentHealth = maxHealth;    
    }

    public void Damaged(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Respawn();
    }

    void Respawn()
    {
        transform.position = respawnPosion;
        currentHealth = maxHealth;
    }
}
