using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penaut : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>() != null)
        {
            PlayerStats status = collision.GetComponent<PlayerStats>();
            status.jumpAir();
            Destroy(gameObject);
        }
    }
}
