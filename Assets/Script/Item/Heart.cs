using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterStatus>() != null)
        {
            CharacterStatus status = collision.GetComponent<CharacterStatus>();
            status.recoverHealth();
            Destroy(gameObject);
        }
    }
}
