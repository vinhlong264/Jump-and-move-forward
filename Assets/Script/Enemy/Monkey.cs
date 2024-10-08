using Extension;
using UnityEngine;

public class Monkey : Enemy
{
    [SerializeField] private float timeIdle;
    [SerializeField] private float timeJump;
    private bool jump;
    private bool moving;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        timeIdle -= Time.deltaTime;
        timeJump -= Time.deltaTime;

        State();

        ColisionCheck();

        animationChange();
    }

    private void State()
    {
        if (timeIdle > 0)
        {
            moving = false;
        }
        else
        {
            moving = true;
            rb.velocity = new Vector2(2 * isFacingDirection, rb.velocity.y);
        }


        if (!isGroundDetected() && !jump)
        {
            jump = true;
            rb.AddForce(new Vector2(0, 18f), ForceMode2D.Impulse);
        }
    }

    private void ColisionCheck()
    {
        if (isWallDetected())
        {
            timeIdle = 1;
            Facing();
        }

        if (isGroundDetected())
        {
            jump = false;
        }
    }

    private void animationChange()
    {
        anim.SetBool("Moving", moving);
        anim.SetBool("isGround", isGroundDetected());
        anim.SetFloat("yVelocity", rb.velocity.y);
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (Character.transform.DotTest(transform, Vector2.down))
        {
            Die();
        }
    }

    public override void Die()
    {
        anim.SetTrigger("isDeath");
        speed = 0;
    }
}
