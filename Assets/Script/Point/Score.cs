using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask mask;

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius , mask);
        if(collider != null)
        {
            target = collider.transform;
            transform.position = Vector2.MoveTowards(transform.position , target.position, 7f* Time.deltaTime);
        }
        else
        {
            target = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterStatus>() != null)
        {
            GameManager.Instance.addScore();
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
