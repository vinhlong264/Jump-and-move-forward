using Extension;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    [Space]
    [SerializeField] private List<Transform> transformTarget = new List<Transform>();
    public int indexTarget = 0;
    public float speed;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (transformTarget.Count >= 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, transformTarget[indexTarget].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, transformTarget[indexTarget].position) < 0.1f)
            {
                facingController(transform.position.x);
                indexTarget++;

                if (indexTarget >= 3)
                {
                    indexTarget = 0;
                }
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (player.transform.DotTest(transform, Vector2.down))
        {
            Die();
        }
        else
        {
            player.hitEntity();
            player.StartCoroutine("isNoJump", 4f);
        }
    }

    public override void Die()
    {
        anim.SetTrigger("isDeath");
    }

    protected override void DestroySelf()
    {
        base.DestroySelf();
    }
}
