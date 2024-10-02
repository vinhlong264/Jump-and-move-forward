using UnityEngine;
using Extension;
using System.Collections.Generic;
public class Enemy : Entity
{
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player != null)
        {
            if (player.transform.DotTest(transform, Vector2.down))
            {
                Debug.Log("Hướng trên: " + collision.transform.DotTest(transform, Vector2.down));
                Destroy(gameObject);
            }
            else
            {
                player.Die();
            }
        }
    }
}
