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
        base.OnCollisionEnter2D(collision);
    }

    protected override void hitCharacter(CharacterStatus character)
    {
        character.takeDame();
    }

    public override void Die()
    {
        anim.SetTrigger("isDeath");
    }
}
