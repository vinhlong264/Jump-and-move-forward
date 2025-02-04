using UnityEngine;

public class Monkey : Enemy
{
    [SerializeField] private float timeIdle;
    [SerializeField] private float timeJump;
    [SerializeField] private bool jump;
    [SerializeField] private Vector2 jumpForce;
    private float timeState;
    private bool moving;
    private bool isJumping;
    protected override void Start()
    {
        base.Start();
        timeState = timeIdle;

    }

    protected override void Update()
    {
        BehaviourEnemy();
        animationChange();
    }


    private void animationChange()
    {
        anim.SetBool("Moving", moving);
        anim.SetBool("isGround", isGroundDetected());
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    protected override void hitCharacter(PlayerStats character)
    {
        character.takeDame();
    }

    public override void Die()
    {
        base.Die();
    }

    protected override void BehaviourEnemy()
    {
        timeState -= Time.deltaTime;
        if (timeState < 0 && isGroundDetected())
        {
            moving = true;
            isJumping = false;
            rb.velocity = new Vector2(speed * isFacingDirection, rb.velocity.y);
            if (isWallDetected())
            {
                moving = false;
                Facing();
                timeState = timeJump;
            }
        }


        if (!isGroundDetected() && !isJumping)
        {
            moving = false;
            isJumping = true;
            timeState = timeIdle;
            rb.AddForce(new Vector2(jumpForce.x * isFacingDirection , jumpForce.y), ForceMode2D.Impulse);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
