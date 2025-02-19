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
        base.OnCollisionEnter2D(collision);
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
        throw new System.NotImplementedException();
    }
}
