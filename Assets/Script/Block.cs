using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Animator anim;
    private Collider2D colider;
    void Start()
    {
        anim = GetComponent<Animator>();
        colider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("DisappearBlock", 3f);
        }
    }

    private void DisappearBlock()
    {
        anim.SetBool("ChangeState", true);
        colider.enabled = false;
    }

    private void recoverBlock()
    {
        anim.SetBool("ChangeState", false);
        colider.enabled = true;
    }

   
      
}
