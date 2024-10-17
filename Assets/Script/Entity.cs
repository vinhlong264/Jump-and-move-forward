using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Colision check info")]
    [SerializeField] protected Transform Ground;
    [SerializeField] protected float distanceToGround;
    [SerializeField] protected LayerMask mask;

    protected Vector2 knockBack;

    protected int isFacingDirection = 1;
    protected bool isFacingRight = true;


    protected Rigidbody2D rb;
    protected Animator anim;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {

    }


    protected virtual void facingController(float x)
    {
        if (x > 0 && !isFacingRight)
        {
            Facing();
        }
        else if (x < 0 && isFacingRight)
        {
            Facing();
        }
    }

    protected virtual void Facing()
    {
        isFacingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    protected bool isGroundDetected() => Physics2D.OverlapCircle(Ground.position, distanceToGround, mask);

    public virtual void Die()
    {

    }

    protected virtual void animatorChange()
    {

    }

    protected virtual IEnumerator isKnockBack(float _second)
    {
        yield return null;
    }


    protected virtual void OnDrawGizmos()
    {
        if (Ground == null) return;

        Gizmos.DrawWireSphere(Ground.position, distanceToGround);
    }
}
