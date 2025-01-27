using Extension;
using UnityEngine;
public class Enemy : Entity
{
    [SerializeField] protected Transform Wall;
    [SerializeField] protected float distanceToWall;
    [SerializeField] protected float speed;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterStatus>() != null)
        {
            CharacterStatus character = collision.gameObject.GetComponent<CharacterStatus>();
            if (character != null)
            {
                if(character.transform.position.y > transform.position.y && collision.GetContact(0).normal.y < 0)
                {
                    Die();
                }
                else
                {
                    hitCharacter(character);
                }
            }
        }
    }

    protected virtual void hitCharacter(CharacterStatus character)
    {

    }


    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected bool isWallDetected() => Physics2D.Raycast(Wall.position, Vector2.right * isFacingDirection ,distanceToWall, mask);

    public override void Die()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("isDeath");
    }

    protected override void OnDrawGizmos()
    {

        base.OnDrawGizmos();

        if(Wall == null) return;

        Gizmos.DrawLine(Wall.position , new Vector3(Wall.position.x + distanceToWall * isFacingDirection , Wall.position.y , Wall.position.z));
    }
}
