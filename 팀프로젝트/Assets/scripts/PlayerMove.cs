using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMove : MonoBehaviour
{
    PlayerHealth playerHealth;
    SpriteRenderer AttackRangeSprite;
    GameObject AttackRange;
    BoxCollider2D Bc;
    Rigidbody2D rb;

    public float AttackDamage = 5f;
    private float _attackDelay = 0.4f;
    private bool _isAttacking = false;
    public float moveSpeed;
    public float currentSpeed;
    public float runSpeed;

    private Vector3 _Movevelocity = Vector3.zero;
    public float curTime;
    public float AttackcoolTime = 0.5f;
    private int _enemyLayer;

    // 방향 저장
    private Vector2 _lastMoveDirection = Vector2.down;

    private void Start()
    {
        currentSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        AttackRange = transform.GetChild(0).gameObject;
        Bc = AttackRange.GetComponent<BoxCollider2D>();
        Bc.isTrigger = true;
        AttackRange.SetActive(false);
        _enemyLayer = LayerMask.NameToLayer("Enemy");
        playerHealth = GetComponent<PlayerHealth>();

        AttackRangeSprite = AttackRange.GetComponent<SpriteRenderer>();
        if (AttackRangeSprite == null)
        {
            AttackRangeSprite = AttackRange.AddComponent<SpriteRenderer>();
        }
        AttackRangeSprite.color = new Color(1f, 0f, 0f, 0.5f); // 반투명 빨간색으로 설정
        AttackRange.SetActive(false);
    }

    void Update()
    {
        Move();

        if (Input.GetKey(KeyCode.F) && !_isAttacking && !playerHealth.isInvincible)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    // Move
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 좌우 이동
        float y = Input.GetAxisRaw("Vertical"); // 상하 이동

        Vector3 _MoveDir = new Vector3(x, y, 0).normalized;

        if (_MoveDir != Vector3.zero)
        {
            _lastMoveDirection = _MoveDir;
        }

        _Movevelocity = _MoveDir * moveSpeed * Time.deltaTime;
        transform.position += _Movevelocity;

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
        _isAttacking = true;
        SetAttackPosition();
        AttackRange.SetActive(true);
        yield return new WaitForSeconds(_attackDelay);
        AttackRange.SetActive(false);
        _isAttacking = false;
    }

    private void SetAttackPosition()
    {
        if (_lastMoveDirection == Vector2.up)
        {
            Bc.offset = new Vector2(0, 1);
            Bc.size = new Vector2(1, 1);
        }
        else if (_lastMoveDirection == Vector2.down)
        {
            Bc.offset = new Vector2(0, -1);
            Bc.size = new Vector2(1, 1);
        }
        else if (_lastMoveDirection == Vector2.left)
        {
            Bc.offset = new Vector2(-1, 0);
            Bc.size = new Vector2(1, 1);
        }
        else if (_lastMoveDirection == Vector2.right)
        {
            Bc.offset = new Vector2(1, 0);
            Bc.size = new Vector2(1, 1);
        }
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _enemyLayer)
        {
            EnemyHealth enemyHp = collision.GetComponent<EnemyHealth>();
            if (enemyHp != null)
            { 
                enemyHp.Damage(AttackDamage);
            }
        }
    }
}
