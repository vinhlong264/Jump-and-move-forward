using Extension;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    [Space]
    [SerializeField] private List<Transform> transformTarget = new List<Transform>();
    private int indexTarget = 0;
    [SerializeField] private int amountTarget;
    protected override void Start()
    {
        base.Start();
        amountTarget = transformTarget.Count;
    }

    protected override void Update()
    {
        if (transformTarget.Count <= 0) return;


        if (transformTarget.Count >= amountTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, transformTarget[indexTarget].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, transformTarget[indexTarget].position) < 0.1f)
            {
                indexTarget++;
                Facing();

                if (indexTarget >= amountTarget)
                {
                    indexTarget = 0;
                }
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void hitCharacter(CharacterStatus character)
    {
        character.takeDame();
        character.StartCoroutine("increaseFallSpeedIn", 3f);
    }

    public override void Die()
    {
        base.Die();
    }
}
