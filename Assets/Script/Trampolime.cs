using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolime : MonoBehaviour
{
    [SerializeField] private Vector2 pushForce;
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(pushForce,ForceMode2D.Impulse);
            animator.SetBool("Force", true);
        }
    }

    private void resetAnimtion()
    {
        animator.SetBool("Force", false);
    }
}
