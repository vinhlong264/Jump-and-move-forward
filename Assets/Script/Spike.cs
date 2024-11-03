using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterStatus>() != null)
        {
            CharacterStatus character = collision.gameObject.GetComponent<CharacterStatus>();
            if(character != null)
            {
                character.takeDame();
            }
        }
    }
}
