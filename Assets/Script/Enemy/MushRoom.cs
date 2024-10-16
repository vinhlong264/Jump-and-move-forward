using Extension;
using UnityEngine;

public class MushRoom : Enemy
{
    protected override void Start()
    {
        base.Start();
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterStatus>() != null)
        {
            CharacterStatus character = collision.gameObject.GetComponent<CharacterStatus>();
            if (character != null)
            {
                if (character.transform.DotDirectionTo(transform, Vector2.down))
                {
                    Die();
                }
                else
                {
                    character.takeDame();
                }
            }
        }
    }

    public override void Die()
    {
        anim.SetTrigger("isDeath");
    }
}
