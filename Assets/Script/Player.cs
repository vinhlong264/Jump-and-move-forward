using UnityEngine;

public class Player : Entity
{
    private Vector3 getDirection;
    public int jumpCount;
    private int currentJumpCount;


    [SerializeField] private GameObject dotPrefabs;
    [SerializeField] private int amountDot;
    [SerializeField] private float betweenSpaceDot;
    [SerializeField] private Transform dotParent;
    [SerializeField] private GameObject[] Dots;


    [SerializeField] private bool isOppositeDirection; // kiểm tra về việc nhảy ngược hướng

    protected override void Start()
    {
        base.Start();
        currentJumpCount = jumpCount;
        InitializeDots();
    }


    protected override void Update()
    {
        InputCharacter();

        if (isGroundDetected())
        {
            jumpCount = currentJumpCount;
        }

        facingController(getMouse().x);

        animatorChange();

    }

    private void InputCharacter()
    {
        if (Input.GetKey(KeyCode.Mouse0) && jumpCount > 0)
        {
            getDirection = getMouse();
            rb.velocity = new Vector2(0, rb.velocity.y * 0.1f);
            dotsActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (jumpCount <= 0) return;


            jumpCount--;
            Jump();
            dotsActive(false);
        }
    }

    private void Jump()
    {
        if (isOppositeDirection)
        {
            rb.AddForce(-getDirection.normalized * 20f, ForceMode2D.Impulse);
            isOppositeDirection = false;
        }
        else
        {
            rb.AddForce(getDirection.normalized * 20f, ForceMode2D.Impulse);
        }
    }

    public override void Die()
    {
        anim.SetTrigger("isDeath");
    }

    private void animatorChange()
    {
        anim.SetBool("isJump", !isGroundDetected());
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private Vector2 getMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePos - transform.position;
    }
    #region animDots
    private void InitializeDots()
    {
        Dots = new GameObject[amountDot];
        for (int i = 0; i < amountDot; i++)
        {
            Dots[i] = Instantiate(dotPrefabs, transform.position, Quaternion.identity, dotParent.transform);
            Dots[i].SetActive(false);
        }
    }

    private void dotsActive(bool _isActive)
    {
        for (int i = 0; i < Dots.Length; i++)
        {
            Dots[i].transform.position = dotsPosition(i * betweenSpaceDot);
            Dots[i].SetActive(_isActive);
        }
    }

    private Vector2 dotsPosition(float _t)
    {
        Vector2 dir = getMouse().normalized;
        return (Vector2)transform.position + dir * _t;
    }

    #endregion
}
