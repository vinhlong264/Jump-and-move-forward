using Extension;
using UnityEngine;
using UnityEngine.XR;

public class Monkey : Enemy
{
    [SerializeField] private float timeIdle = 2;
    [SerializeField] private bool jump;
    [SerializeField] private float jumpForce;
    private bool moving;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        timeIdle -= Time.deltaTime;

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
            rb.velocity = new Vector2(speed * isFacingDirection, rb.velocity.y);
        }


        if (!isGroundDetected() && !jump)
        {
            jump = true;
            rb.velocity = new Vector2(rb.velocity.x , jumpForce);
        }
    }

    private void ColisionCheck()
    {
        if (isWallDetected())
        {
            timeIdle = 2;
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
        if(collision.gameObject.GetComponent<CharacterStatus>() != null)
        {           
            CharacterStatus character = collision.gameObject.GetComponent<CharacterStatus>();
            if(character != null)
            {
                if (character.transform.DotTest(transform, Vector2.down))
                {
                    Die();
                }
                else
                {
                    character.takeDame();
                    character.reverseDirection();
                }
            }
        }
    }

    public override void Die()
    {
        base.Die();
        anim.SetTrigger("isDeath");
    }
}
