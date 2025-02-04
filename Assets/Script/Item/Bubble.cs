using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerStats>() != null)
        {
            PlayerStats status = collision.GetComponent<PlayerStats>();
            status.StartCoroutine("removeGravityBy", 3f);
            Destroy(gameObject);
        }
    }
}
