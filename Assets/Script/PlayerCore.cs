using UnityEngine;
using UnityEngine.UI;

public class PlayerCore : MonoBehaviour
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
    private Vector3 mousePos;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float direction;
    [SerializeField] private float isDirRight;
    private bool isExcute;

    [SerializeField] private Button left;
    [SerializeField] private Button right;
    void Start()
    {
        isDoubleJump = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();


        left.onClick.AddListener(() =>
        {
            isExcute = true;
            isDirRight = -1;
            rb.velocity = Vector3.zero;
        });

        right.onClick.AddListener(() =>
        {
            isExcute = true;
            isDirRight = 1;
            rb.velocity = Vector3.zero;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (isExcute && !isWall)
        {
            rb.velocity =  new Vector2(jumpForce.x * isDirRight , jumpForce.y);
            isExcute = false;
        }

        if (isWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
            if (isExcute)
            {
                rb.velocity = new Vector2(jumpForce.x * isDirRight , jumpForce.y);
                isExcute = false;
            }
        }
    }

    private void MainFunction()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.z = 0;
        dir.Normalize();

        if (dir.x > transform.position.x)
        {
            isDirRight = 1f;
        }
        else
        {
            isDirRight = -1f;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isJumping)
        {
            if (isJumping && (dir * jumpForce).y < 0) return;
            isJumping = false;
            rb.velocity = dir * jumpForce;
            isDoubleJump = true;
            return;
        }

        if (isDoubleJump && !isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CanDoubleJump = true;
                isDoubleJump = false;
                rb.velocity = dir * jumpForce;
            }
        }

        if (isWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                rb.velocity = dir * jumpForce;
                isWall = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }

        if (hit() && isDirRight > 0)
        {
            transform.Rotate(0, 180, 0);
        }
        else if (hit() && isDirRight < 0)
        {
            transform.Rotate(0, -180, 0);
        }

        animationHandler();

        isExcute = false;

        return;
    }

    RaycastHit2D hit() => Physics2D.Raycast(transform.position, Vector2.right * isDirRight, direction, mask);

    private void animationHandler()
    {
        animator.SetBool("Ground", isJumping);
        animator.SetBool("JumpWall", isWall);
        animator.SetBool("DoubleJump", CanDoubleJump);
        animator.SetFloat("yVelocity", rb.velocity.y);

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position , new Vector3(transform.position.x + direction * isDirRight , transform.position.y , transform.position.z));  
    }
}
