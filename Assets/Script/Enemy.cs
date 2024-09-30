using UnityEngine;
using Extension;
using System.Collections.Generic;
public class Enemy : Entity
{
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                Debug.Log("Hướng trên: " + collision.transform.DotTest(transform, Vector2.down));
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Hướng ngang: " + collision.transform.DotTest(transform, Vector2.down));
                Destroy(collision.gameObject);
            }
        }
    }
}
