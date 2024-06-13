using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float maxSpeed;
    public float jump;
    Vector3 vec;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 2.0f;
    }
    private void FixedUpdate()
    {
        Move();
    }
    //private void Update()
    //{
    //    Jump();
    //}

    public void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate((new Vector3(h, v, 0) * maxSpeed) * Time.deltaTime);
    }
    //public void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        rigid.AddForce(Vector3.up*jump, ForceMode2D.Impulse);
    //    }
    //}
}
