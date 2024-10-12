using UnityEngine;

public class Enemy_Dealine : Enemy
{
    protected override void Start()
    {
        base.Start();
    }


    private void FixedUpdate()
    {
        rb.velocity = Vector2.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterStatus>() != null)
        {
            CharacterStatus character = collision.GetComponent<CharacterStatus>();

            if (character != null)
            {
                character.Die();
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
    }
}
