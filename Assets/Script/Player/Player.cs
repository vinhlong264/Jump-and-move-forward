using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Vector2 finalDirection;
    [SerializeField] private Vector2 laughDirection;
    [SerializeField] private int jumpCount;
    [SerializeField] private int currentJumpCount;
    [SerializeField] private GameObject effectPrefabs;

    [Header("Anim Dot")]
    [SerializeField] private GameObject dotPrefabs;
    [SerializeField] private int amountDot;
    [SerializeField] private float betweenSpaceDot;
    [SerializeField] private Transform dotParent;
    [SerializeField] private GameObject[] Dots;

    //Harmful effects
    private bool isOppositeDirection;// Biến tạo hiệu ứng nhảy ngược hướng
    private bool isJump; // kiểm tra có đang nhảy không

    private CharacterStatus characterStatus;

    protected override void Start()
    {
        base.Start();
        characterStatus = GetComponent<CharacterStatus>();

        currentJumpCount = jumpCount;
        knockBack = new Vector2(5, 10);
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
            finalDirection = new Vector2(getMouse().normalized.x * laughDirection.x , getMouse().normalized.y * laughDirection.y);

            for (int i = 0; i < Dots.Length; i++)
            {
                Dots[i].transform.position = dotsPosition(i * betweenSpaceDot);
            }

            rb.velocity = new Vector2(0, rb.velocity.y * characterStatus.airJump);
            dotsActive(true);
        }

        if (Input.GetMouseButtonUp(0) && currentJumpCount > 0)
        {
            //AudioManager.Instance.PlaySound(SoundType.JUMP, 1);
            Jump(finalDirection);
            currentJumpCount--;
            dotsActive(false);
        }
    }

    private void Jump(Vector2 _dir)
    {

        if (isOppositeDirection)
        {
            StartCoroutine(JumpForce(finalDirection));
            isOppositeDirection = false;
        }
        else
        {         
            StartCoroutine(JumpForce(finalDirection));
        }
    }


    IEnumerator JumpForce(Vector2 dir)
    {
        isJump = true;
        rb.velocity = new Vector2(dir.x, dir.y * 0.6f);
        yield return new WaitForSeconds(0.3f);
        if (!isGroundDetected())
        {
            GameObject effectObj = Instantiate(effectPrefabs, Ground.position, Quaternion.identity);
        }

        rb.AddForce(dir, ForceMode2D.Impulse);
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
        anim.SetBool("isJump", isJump);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGroundDetected())
        {
            currentJumpCount = jumpCount;
            isJump = false;
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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        return mousePos - playerPos;
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
            Dots[i].SetActive(_isActive);
        }
    }

    private Vector2 dotsPosition(float _t)
    {
        Vector2 dir = new Vector2(getMouse().normalized.x * laughDirection.x , getMouse().normalized.y * laughDirection.y);

        Vector2 postion = (Vector2)transform.position + dir * _t + 0.5f * (Physics2D.gravity * rb.gravityScale) * Mathf.Pow(_t, 2);

        return postion;
    }
    #endregion
}
