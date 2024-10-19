using System.Collections;
using UnityEngine;

public class Player : Entity
{
    private Vector3 getDirection;
    [SerializeField] private int jumpCount;
    [SerializeField] private int currentJumpCount;
    [SerializeField] private float jumpForce;

    [Header("Anim Dot")]
    [SerializeField] private GameObject dotPrefabs;
    [SerializeField] private int amountDot;
    [SerializeField] private float betweenSpaceDot;
    [SerializeField] private Transform dotParent;
    [SerializeField] private GameObject[] Dots;

    //Harmful effects
    private bool isOppositeDirection;// Biến tạo hiệu ứng nhảy ngược hướng

    private CharacterStatus characterStatus;

    protected override void Start()
    {
        base.Start();
        characterStatus = GetComponent<CharacterStatus>();
        currentJumpCount = jumpCount;
        knockBack = new Vector2(5, 15);
        InitializeDots();
    }

    public void setUpPlayer(bool _isOppositeDirection , float _gravity)
    {
        isOppositeDirection = _isOppositeDirection;
        rb.gravityScale = _gravity;
    }


    protected override void Update()
    {

        InputCharacter();

        facingController(getMouse().x);

        animatorChange();

        if (isGroundDetected() && currentJumpCount <= 0)
        {
            currentJumpCount = jumpCount;
        }
        Observer.Instance.Notify(ActionType.JumpCount, currentJumpCount);
    }

    private void InputCharacter()
    {
        if (characterStatus.noJump || currentJumpCount <= 0) return;


        if (Input.GetMouseButton(0) && currentJumpCount > 0)
        {
            getDirection = getMouse();
            rb.velocity = new Vector2(0, rb.velocity.y * characterStatus.airJump);
            dotsActive(true);
        }

        if (Input.GetMouseButtonUp(0) && currentJumpCount > 0)
        {
            Jump();
            currentJumpCount--;
            dotsActive(false);
        }
    }

    private void Jump()
    {

        if (isOppositeDirection)
        {
            rb.AddForce(-getDirection.normalized * jumpForce, ForceMode2D.Impulse);
            isOppositeDirection = false;
        }
        else
        {
            rb.AddForce(getDirection.normalized * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void activeJumpingAir()
    {
        jumpCount++;
        currentJumpCount++;
    }

    public override void Die()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("isDeath");
    }

    protected override void animatorChange()
    {
        anim.SetBool("isJump", !isGroundDetected());
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGroundDetected())
        {
            currentJumpCount = jumpCount;
        }
    }

    public void isHit()
    {
        anim.SetTrigger("isHit");
        StartCoroutine("isKnockBack", 0.07f);
    }
    protected override IEnumerator isKnockBack(float _second)
    {
        rb.velocity = new Vector2(knockBack.x * -isFacingDirection, knockBack.y);
        yield return new WaitForSeconds(_second);
    }

    private Vector2 getMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        return direction;
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
