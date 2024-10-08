using Extension;
using UnityEngine;
public class Enemy : Entity
{
    [SerializeField] protected Transform Wall;
    [SerializeField] protected float distanceToWall;
    [SerializeField] protected float speed;
    [SerializeField] protected CharacterStatus Character;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Character = collision.gameObject.GetComponent<CharacterStatus>();
        if (Character == null) return;
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected bool isWallDetected() => Physics2D.Raycast(Wall.position, Vector2.right * isFacingDirection ,distanceToWall, mask);

    public override void Die()
    {
    }

    protected override void OnDrawGizmos()
    {

        base.OnDrawGizmos();

        if(Wall == null) return;

        Gizmos.DrawLine(Wall.position , new Vector3(Wall.position.x + distanceToWall * isFacingDirection , Wall.position.y , Wall.position.z));
    }
}
