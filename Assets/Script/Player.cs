using System.Collections;
using UnityEngine;

public class Player : Entity
{
    private SpriteRenderer sr;
    private Vector3 getDirection;
    public int jumpCount;
    private int currentJumpCount;
    private float airJump = 0.1f;
    private float currentAirJump;

    [Header("Anim Dot")]
    [SerializeField] private GameObject dotPrefabs;
    [SerializeField] private int amountDot;
    [SerializeField] private float betweenSpaceDot;
    [SerializeField] private Transform dotParent;
    [SerializeField] private GameObject[] Dots;

    //Harmful effects
    private bool isOppositeDirection;// Biến tạo hiệu ứng nhảy ngược hướng
    private bool noJump; // Biến tạo không thể nhảy
    

    protected override void Start()
    {
        base.Start();

        sr = GetComponent<SpriteRenderer>();
        currentJumpCount = jumpCount;
        currentAirJump = airJump;
        knockBack = new Vector2(3, 10);

        InitializeDots();
    }


    public void setUpPlayer(bool _isOppositeDirection)
    {
        _isOppositeDirection = isOppositeDirection;
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
        if (noJump) return;


        if (Input.GetKey(KeyCode.Mouse0) && jumpCount > 0)
        {
            getDirection = getMouse();
            rb.velocity = new Vector2(0, rb.velocity.y * airJump);
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


    public void hitEntity()
    {
        StartCoroutine("isKnockBack");
    }

    public override void Die()
    {
        anim.SetTrigger("isDeath");
        rb.bodyType = RigidbodyType2D.Static;
    }

    protected override void animatorChange()
    {
        anim.SetBool("isJump", !isGroundDetected());
        anim.SetFloat("yVelocity", rb.velocity.y);
    }
    #region StartCroutine
    protected override IEnumerator isKnockBack()
    {
        anim.SetTrigger("isHit");
        rb.velocity = new Vector2(knockBack.x * -isFacingDirection, knockBack.y);
        yield return new WaitForSeconds(0.07f);
    }

    private IEnumerator isNoJump(float _second) // hiệu ứng gây hại: Xóa khả năng nhảy
    {
        noJump = true;
        yield return new WaitForSeconds(_second);
        noJump = false;
    }

    private IEnumerator increaseFallSpeed(float _second) // hiệu ứng gây hại: Rơi nhanh hơn khi ở trạng thái trên không
    {
        airJump = 0.9f;
        sr.color = Color.red;
        yield return new WaitForSeconds(_second);
        airJump = currentAirJump;
        sr.color = new Color(1, 1, 1);
    }

    #endregion

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
