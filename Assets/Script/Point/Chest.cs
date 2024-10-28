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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("openChest");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterStatus>() != null)
        {
            if (collision.transform.DotDirectionTo(transform, Vector2.down))
            {
                Debug.Log("Dot > 0");
                animator.SetTrigger("openChest");
            }
            else
            {
                Debug.Log("Dot < 0 || Dot == 0");
            }
        }
    }

    private void openChest()
    {
        GameObject newScore = Instantiate(scorePrefabs, transform.position , Quaternion.identity);

        Vector2 velocity = new Vector2(Random.Range(-5,5),Random.Range(12,15));

        newScore.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
