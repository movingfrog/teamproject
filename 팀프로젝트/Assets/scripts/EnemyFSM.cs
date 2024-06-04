using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public float enemycurrentSpeed;
    public float enemychaseSpeed;
    public float chaseRange;
    public float attackRange;
    public float changeDir = 1.2f;

    private Transform target;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private float ChangeTime;

    private enum State
    {
        IDLE,
        CHASE
    }
    private State currentState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.IDLE;
        ChangeTime = changeDir;
        ChangeDirection();
    }

    private void Update()
    {
        ChangeTime -= Time.deltaTime;

        if (ChangeTime <= 0)
        {
            ChangeDirection();
            ChangeTime = changeDir;
        }

        switch (currentState)
        {
            case State.IDLE:
                Idle();
                break;
            case State.CHASE:
                Chase();
                break;
        }
    }

    private void Idle()
    {
        rb.velocity = moveDir * enemycurrentSpeed;

        if (Vector2.Distance(transform.position, target.position) <= chaseRange)
        {
            currentState = State.CHASE;
        }
    }

    private void Chase()
    {
        if (Vector2.Distance(transform.position, target.position) > chaseRange)
        {
            currentState = State.IDLE;
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * enemychaseSpeed;
    }

   

    private void ChangeDirection()
    {
        int randomDir = Random.Range(0, 8);
        switch (randomDir)
        {
            case 0:
                moveDir = Vector2.up; // Move Up
                break;
            case 1:
                moveDir = Vector2.down; // Move Down
                break;
            case 2:
                moveDir  = Vector2.left; // Move Left
                break;
            case 3:
                moveDir = Vector2.right; // Move Right
                break;
            case 4:
                moveDir  = new Vector2(1, 1).normalized; // Diagonal Top Right
                break;
            case 5:
                moveDir = new Vector2(-1, 1).normalized; // Diagonal Top Left
                break;
            case 6:
                moveDir = new Vector2(1, -1).normalized; // Diagonal Down Right
                break;
            case 7:
                moveDir = new Vector2(-1, -1).normalized; // Diagonal Down Left
                break;
        }
    }
}

