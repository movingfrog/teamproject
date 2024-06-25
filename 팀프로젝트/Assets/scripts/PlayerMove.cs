using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    PlayerHealth playerHealth;
    SpriteRenderer AttackRangeSprite;
    SpriteRenderer sprite;
    GameObject AttackRange;
    Rigidbody2D rb;
    BoxCollider2D Bc;
    Animator anim;
    [SerializeField] Image menu;

    public float AttackDamage = 5f;
    private float _attackDelay = 0.4f;
    private bool _isAttacking = false;
    public float moveSpeed;
    public float currentSpeed;
    public float runSpeed;
    public Vector2 inputVec;

    private Vector3 _Movevelocity = Vector3.zero;
    public float curTime;
    public float AttackcoolTime = 0.5f;
    private int _enemyLayer;
    

    // 방향 저장
    private Vector2 _lastMoveDirection = Vector2.down;

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentSpeed = moveSpeed;
        AttackRange = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Bc = AttackRange.GetComponent<BoxCollider2D>();
        Bc.isTrigger = true;
        AttackRange.SetActive(false);
        _enemyLayer = LayerMask.NameToLayer("Enemy");
        playerHealth = GetComponent<PlayerHealth>();

        AttackRangeSprite = AttackRange.GetComponent<SpriteRenderer>();
        AttackRangeSprite.color = new Color(1f, 0f, 0f, 0.5f); // 반투명 빨간색으로 설정
        AttackRange.SetActive(false);
    }

    void Update()
    {
        Menu();
        if (Input.GetKey(KeyCode.F) && !_isAttacking && !playerHealth.isInvincible)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private void FixedUpdate()
    {
        Vector2 _nextMove = inputVec.normalized * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + _nextMove);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            sprite.flipX = inputVec.x < 0;
        }
    }

    // Move
    void OnMove(InputValue value)
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        inputVec = value.Get<Vector2>();
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

    private void Menu()
    {
        if (Input.GetKey(KeyCode.Escape) && !playerHealth.isDie)
        {
            menu.gameObject.SetActive(true);
        }
    }
}
