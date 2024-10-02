using Extension;
using UnityEngine;
public class Enemy : Entity
{
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            if (player.transform.DotTest(transform, Vector2.down))
            {
                //EnemyDeath
            }
            else
            {
                player.Die();
                player.StartCoroutine("increaseFallSpeed", 3f);
            }
        }
    }
}
