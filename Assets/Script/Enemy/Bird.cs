using System.Collections;
using UnityEngine;

public class Bird : Enemy
{
    private Vector3 posTarget;
    [SerializeField] private float radius;
    private bool isSearchPos;
    private float timer;
    private Vector2 posDestination;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        BehaviourEnemy();
    }

    protected override void BehaviourEnemy()
    {
        timer -= Time.deltaTime;
        if (!isSearchPos && timer < 0)
        {
            float randomX = Random.Range(-3, 3);
            float randomY = Random.Range(-3, 3);
            Vector2 dumpPos = new Vector2(randomX + transform.position.x, randomY + transform.position.y);
            Debug.Log("Tìm được điểm đến: " + Physics2D.OverlapCircle(dumpPos, radius, mask));
            if(!Physics2D.OverlapCircle(dumpPos , radius , mask))
            {
                posDestination = dumpPos;
                isSearchPos = true;
            }
        }

        if (isSearchPos)
        {
            if(transform.position.x < posDestination.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else if(transform.position.x > posDestination.x)
            {
                transform.localScale = new Vector2(1, 1);
            }
            StartCoroutine(Movement());
        }
    }


    IEnumerator Movement()
    {
        yield return new WaitForSeconds(1f);
        transform.position = Vector2.MoveTowards(transform.position, posDestination, 2f * Time.deltaTime);
        if (Vector2.Distance(transform.position, posDestination) < 0.1f)
        {
            isSearchPos = false;
            timer = 5f;
        }
    }

    protected override void hitCharacter(PlayerStats character)
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
