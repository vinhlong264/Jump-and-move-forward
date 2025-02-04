using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            PlayerStats character = collision.gameObject.GetComponent<PlayerStats>();
            if(character != null)
            {
                character.takeDame();
            }
        }
    }
}
