using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private CapsuleCollider2D cp;
    public float enemySpeed;
    public int enemyHp;
    public int range;
    public float currentSpeed;
    private int NextMove;
    public float stopDistance;

    private Transform target;

    private void Awake()
    {
        cp = GetComponent<CapsuleCollider2D>();


    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
       if (Vector2.Distance(transform.position, target.position) > stopDistance )
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
        }
    }

    private void enemyMove()
    {

    }

    private void Think()
    {
        // Next Move
        NextMove = Random.Range(-1, 2);

        //Idle
        if (NextMove == 0)
        {
           
        }
    }
}
