using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject AttackRange;
    BoxCollider2D Bc;
    Rigidbody2D rb;

    public float AttackDamage = 5f;
    private float attackDelay = 0.4f;
    private bool isAttacking = false;
    public float moveSpeed;
    public float currentSpeed;
    public float runSpeed;

    private Vector3 Movevelocity = Vector3.zero;
    public float curTime;
    public float AttackcoolTime = 0.5f;
    private int enemyLayer;

    private void Start()
    {
        currentSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        AttackRange = transform.GetChild(0).gameObject;
        Bc = AttackRange.GetComponent<BoxCollider2D>();
        Bc.isTrigger = true;
        AttackRange.SetActive(false);
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    void Update()
    {
        Move();

        if (Input.GetKey(KeyCode.F) && !isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    // Move
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 좌우 이동
        float y = Input.GetAxisRaw("Vertical"); // 상하 이동

        Movevelocity = new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
        transform.position += Movevelocity;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = currentSpeed;
        }
    }

    // Attack
    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        AttackRange.SetActive(true);
        yield return new WaitForSeconds(attackDelay);
        AttackRange.SetActive(false);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            EnemyHealth enemyHp = collision.GetComponent<EnemyHealth>();
            if (enemyHp != null)
            { 
                enemyHp.Damage(AttackDamage);
            }
        }
    }
}
