using Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    private Vector3 posTarget;
    [SerializeField] private float radius;
    private bool isSearchPos;
    private float timer;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (!isSearchPos)
            {
                posTarget = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5));
                Collider2D col = Physics2D.OverlapCircle(posTarget, radius, mask);
                if (col == null)
                {
                    isSearchPos = true;
                }
            }
        }
        
        if (isSearchPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, posTarget, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, posTarget) < 0.1f)
            {
                isSearchPos = false;
                timer = 4f;
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
    }

    public override void Die()
    {
        base.Die();
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(posTarget, radius);
    }
}
