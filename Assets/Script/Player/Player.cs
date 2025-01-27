using System.Collections;
using UnityEngine;

public class Player : Entity
{
    private bool isTouch;
    private Vector2 finalDirection;
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
    private bool isJump;


    private SpriteRenderer sr;
    private Material currentMaterial;
    [SerializeField] private Material flashMaterial;

    protected override void Start()
    {
        base.Start();

        sr = GetComponentInChildren<SpriteRenderer>();
        currentMaterial = sr.material;
        currentJumpCount = jumpCount;
        isTouch = true;
        knockBack = new Vector2(5, 10);
        InitializeDots();
    }

    protected override void Update()
    {
        if (CharacterStatus.Instance.noJump) return;

        InputCharacter();

        facingController(getMouse().x);

        animatorChange();
        Observer.Instance.Notify(ActionType.JumpCount, currentJumpCount);
    }

    private void InputCharacter()
    {
        if (!isTouch) return;


        if (Input.GetMouseButton(0) && currentJumpCount > 0)
        {
            finalDirection = new Vector2(getMouse().x * laughDirection.x, getMouse().y * laughDirection.y);
            for (int i = 0; i < Dots.Length; i++)
            {
                Dots[i].transform.position = dotsPosition(i * betweenSpaceDot);
            }
            rb.velocity = new Vector2(0, rb.velocity.y * 0.3f);
            dotsActive(true);
        }

        if (Input.GetMouseButtonUp(0) && currentJumpCount > 0)
        {
            AudioManager.Instance.PlaySound(SoundType.JUMP, 1);
            Jump(finalDirection);
            StartCoroutine(chechTouch());
            currentJumpCount--;
            dotsActive(false);
        }
    }

    IEnumerator chechTouch()
    {
        isTouch = false;
        yield return new WaitForSeconds(1f);
        isTouch = true;
    }

    private void Jump(Vector2 _dir)
    {
        StartCoroutine(JumpForce(_dir));
    }


    IEnumerator JumpForce(Vector2 dir)
    {
        isJump = true;
        rb.velocity = new Vector2(dir.x, dir.y * 0.5f);
        yield return new WaitForSeconds(0.3f);
        if (!isGroundDetected())
        {
            GameObject effectObj = ObjectPooling.Instance.GetObj(effectPrefabs);
            if (effectObj != null)
            {
                effectObj.transform.position = Ground.position;
                effectObj.transform.rotation = Quaternion.identity;
            }
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
        sr.material = flashMaterial;
        rb.velocity = new Vector2(knockBack.x * -isFacingDirection, knockBack.y);
        yield return new WaitForSeconds(_second);
        sr.material = currentMaterial;
    }

    private Vector2 getMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        return (mousePos - playerPos).normalized;
    }
    #region animDots
    private void InitializeDots()
    {
        Dots = new GameObject[amountDot];
        for (int i = 0; i < amountDot; i++)
        {
            Dots[i] = Instantiate(dotPrefabs, dotParent.position, Quaternion.identity, dotParent.transform);
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
        Vector2 dir = finalDirection;

        Vector2 postion = (Vector2)transform.position + dir * _t + 0.5f * (Physics2D.gravity * rb.gravityScale) * Mathf.Pow(_t, 2);

        return postion;
    }
    #endregion
}
