using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
   public GameManager gameManager;

    public float moveSpeed;
    private Vector3 Movevelocity = Vector3.zero;
    CapsuleCollider2D cp;

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
        float x = Input.GetAxisRaw("Horizontal"); // 좌우 이동
        float y = Input.GetAxisRaw("Vertical"); // 상하 이동

        Movevelocity = new Vector3(x, y, 0) * moveSpeed *Time.deltaTime;
        transform.position += Movevelocity;
    }
}
