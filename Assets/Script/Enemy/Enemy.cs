using Extension;
using UnityEngine;
public class Enemy : Entity
{
    protected Player player;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();

        if (player == null) return;
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
