using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Colision check info")]
    [SerializeField] protected Transform Ground;
    [SerializeField] protected float distance;
    [SerializeField] protected LayerMask mask;

    [SerializeField] protected Vector2 knockBack;


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
            isFacingController();
        }
        else if (x < 0 && isFacingRight)
        {
            isFacingController();
        }
    }

    protected virtual void isFacingController()
    {
        isFacingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    protected RaycastHit2D isGroundDetected() => Physics2D.Raycast(Ground.position, Vector2.down, distance, mask);

    public virtual void Die()
    {

    }

    protected virtual void animatorChange()
    {

    }

    protected virtual IEnumerator isKnockBack()
    {
        yield return null;
    }


    protected virtual void OnDrawGizmos()
    {
        if (Ground == null) return;

        Gizmos.DrawLine(Ground.position, new Vector3(Ground.position.x, Ground.position.y - distance, 0));
    }
}
