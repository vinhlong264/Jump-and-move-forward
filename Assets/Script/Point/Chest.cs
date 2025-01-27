using Extension;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject scorePrefabs;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterStatus>() != null)
        {
            if(collision.transform.position.y > transform.position.y && collision.GetContact(0).normal.y < 0)
            {
                animator.SetTrigger("openChest");
                openChest();
            }
        }
    }

    private void openChest()
    {
        GameObject newScore = ObjectPooling.Instance.GetObj(scorePrefabs);
        if (newScore != null)
        {
            newScore.transform.position = transform.position;   
            newScore.transform.rotation = Quaternion.identity;
        }

        Vector2 velocity = new Vector2(Random.Range(-5,5),Random.Range(12,15));

        newScore.GetComponent<Rigidbody2D>().velocity = velocity;
        GetComponent<Collider2D>().isTrigger = true;
    }
}
