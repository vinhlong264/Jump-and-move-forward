using Extension;
using UnityEngine;
public class Enemy : Entity
{
    [SerializeField] protected Transform Wall;
    [SerializeField] protected float distanceToWall;
    [SerializeField] protected float speed;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
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
