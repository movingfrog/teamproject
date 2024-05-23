using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Awake()
    {
        cp = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        float distanceTotarget = Vector2.Distance(transform.position, target.position);

        if (distanceTotarget <= range && distanceTotarget > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
           
        }
    }

   private void Idle()
    {

    }
}
