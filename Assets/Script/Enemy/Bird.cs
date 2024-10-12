using Extension;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    [Space]
    [SerializeField] private List<Transform> transformTarget = new List<Transform>();
    public int indexTarget = 0;
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
        if (collision.gameObject.GetComponent<CharacterStatus>() != null)
        {
            CharacterStatus character = collision.gameObject.GetComponent<CharacterStatus>();
            if (character != null)
            {
                if (character.transform.DotTest(transform, Vector2.down))
                {
                    Die();
                }
                else
                {
                    character.takeDame();
                    character.StartCoroutine("increaseFallSpeedIn", 3f);
                }
            }
        }
    }

    public override void Die()
    {
        base.Die();
    }
}
