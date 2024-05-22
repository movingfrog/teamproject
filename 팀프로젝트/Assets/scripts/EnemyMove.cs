using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Vector3 Movevel =  Vector3.zero;
    private CapsuleCollider2D cp;
    public float enemySpeed;
    public int range;
    private int NextMove;

    private void Awake()
    {
        cp = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
       
    }
}
