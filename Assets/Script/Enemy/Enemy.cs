using Extension;
using UnityEngine;
public class Enemy : Entity
{
    protected CharacterStatus character;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        character = collision.gameObject.GetComponent<CharacterStatus>();

        if (character == null) return;
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
