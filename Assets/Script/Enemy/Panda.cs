using Extension;
using UnityEngine;

public class Panda : Enemy
{
    private float timeIdle = 2;
    private float timeCurrent;
    private bool isMoving;
    protected override void Start()
    {
        base.Start();
        timeCurrent = timeIdle;
    }

    protected override void Update()
    {
        timeIdle -= Time.deltaTime;
        stateChange();
        animatorChange();
    }

    private void stateChange()
    {
        if (timeIdle > 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
            rb.velocity = new Vector2(speed * isFacingDirection, rb.velocity.y);
        }

        if (isWallDetected() || !isGroundDetected())
        {
            timeIdle = timeCurrent;
            Facing();
        }
    }

    protected override void animatorChange()
    {
        anim.SetBool("moving", isMoving);
        anim.SetInteger("stateCount", Random.Range(0,10));
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    protected  override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterStatus>() != null)
        {
            CharacterStatus character = collision.gameObject.GetComponent<CharacterStatus>();
            if (character != null)
            {
                if (character.transform.DotDirectionTo(transform, Vector2.down))
                {
                    Die();
                }
                else
                {
                    character.takeDame();
                    character.StartCoroutine("stopJumpIn",2f);
                }
            }
        }
    }

    public override void Die()
    {
        base.Die();
    }
}
