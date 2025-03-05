using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 jumpForce;
    private bool isDoubleJump;
    private bool isJumping;
    private bool isWall;
    private Rigidbody2D rb;
    void Start()
    {
        isDoubleJump = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.z = 0; // Đảm bảo không ảnh hưởng bởi trục Z
        dir.Normalize();

        if (Input.GetKeyDown(KeyCode.Mouse0) && isJumping)
        {
            isJumping = false;
            isDoubleJump = true;
            rb.velocity = dir * jumpForce;
            return;
        }

        if(isDoubleJump && !isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                rb.velocity = dir * jumpForce;
                isDoubleJump = false;
            }
        }

        if (isWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                rb.velocity = dir * jumpForce;
                isWall = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
            isDoubleJump = false;
            isWall = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isWall = true;
        }

    }
}
