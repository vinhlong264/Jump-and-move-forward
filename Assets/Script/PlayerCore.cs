using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerCore: MonoBehaviour
{
    [SerializeField] private Vector2 jumpForce;
    private bool isDoubleJump;
    private bool isJumping;
    private bool isWall;
    private bool CanDoubleJump;
    private bool isGround;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    void Start()
    {
        isDoubleJump = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        

        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.z = 0;
        dir.Normalize();
        if (Input.GetKeyDown(KeyCode.Mouse0) && isJumping)
        {
            Debug.Log((dir * jumpForce).magnitude);
            if (isJumping && (dir * jumpForce).y < 0) return;



            if (dir.x > transform.position.x)
            {
                sr.flipX = false;
            }
            else if (dir.x < transform.position.x)
            {
                sr.flipX = true;
            }
            isJumping = false;
            rb.velocity = dir * jumpForce;
            isDoubleJump = true;
            return;
        }

        if(isDoubleJump && !isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(dir.x > transform.position.x)
                {
                    sr.flipX = false;
                }else if(dir.x < transform.position.x)
                {
                    sr.flipX = true;
                }


                CanDoubleJump = true;
                isDoubleJump = false;
                rb.velocity = dir * jumpForce;
            }
        }

        if (isWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (dir.x > transform.position.x)
                {
                    sr.flipX = false;
                }
                else if (dir.x < transform.position.x)
                {
                    sr.flipX = true;
                }
                rb.velocity = dir * jumpForce;
                isWall = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        animationHandler();
    }

    private void animationHandler()
    {
        animator.SetBool("Ground", isJumping);
        animator.SetBool("JumpWall", isWall);
        animator.SetBool("DoubleJump", CanDoubleJump);
        animator.SetFloat("yVelocity" , rb.velocity.y);

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
            isGround = true;
            isDoubleJump = false;
            isWall = false;
            CanDoubleJump = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isWall = true;
            isGround = false;
            isDoubleJump = false;
            isJumping = false;
            CanDoubleJump = false;
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWall = false;
        }
    }
}
