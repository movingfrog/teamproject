using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ExplosionEnemy : MonoBehaviour
{
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    // Attack
    public float AttackRange = 4f;
    public float ExplodeDelay = 0.3f;
    public float AttackSpeed = 3f;
    public float increaseSpeed = 1f;
    public float ChaseRange;
    public float maxDamage = 50f;
    public float minDamage = 10f;

    Transform player;
    private bool isExploding = false;   

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (!isExploding)
        {
            playerFollow();
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            StartCoroutine(ExplodeAttack());
        }
    }

    private void playerFollow()
    {
        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        transform.position += dir * AttackSpeed * Time.deltaTime;
    }
    
    public IEnumerator ExplodeAttack()
    {
        isExploding = true;
        
        yield return new WaitForSeconds(ExplodeDelay);

        if (playerHealth != null)
        {
            float damage = CaculateDamage();
            playerHealth.TakeDamage(damage);
        }

        // ÀÚÆø ÈÄ Ã¼·Â¹Ù ºñÈ°¼ºÈ­
        if (enemyHealth != null)
        {
            enemyHealth.HideHealthBar();
        }

        // ÀÚÆø ÈÄ ÆÄ±«
        Destroy();
    }

    private float CaculateDamage()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        float damage =  Mathf.Lerp(minDamage, maxDamage, 1 - (distance / AttackRange));
        return damage;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
