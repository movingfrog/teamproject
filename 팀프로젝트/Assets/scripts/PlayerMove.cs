using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float currentSpeed;
    public float runSpeed;
    private Vector3 Movevelocity = Vector3.zero;
    CapsuleCollider2D cp;

    private void Start()
    {
        currentSpeed = moveSpeed;
    }

    private void Awake()
    {
        cp = GetComponent<CapsuleCollider2D>();        
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal"); // �¿� �̵�
        float y = Input.GetAxisRaw("Vertical"); // ���� �̵�

        Movevelocity = new Vector3(x, y, 0) * moveSpeed *Time.deltaTime;
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

    
}
