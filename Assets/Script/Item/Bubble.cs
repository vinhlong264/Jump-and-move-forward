using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterStatus>() != null)
        {
            CharacterStatus status = collision.GetComponent<CharacterStatus>();
            status.StartCoroutine("removeGravityBy", 3f);
            Destroy(gameObject);
        }
    }
}
