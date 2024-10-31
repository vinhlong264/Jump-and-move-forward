using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angry_bullet : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;

    public void AttackTarget(Transform _targetPos , float _moveSpeed)
    {
       target = _targetPos;
        moveSpeed = _moveSpeed;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterStatus>() != null)
        {
            CharacterStatus character = collision.GetComponent<CharacterStatus>();
            if(character != null)
            {
                character.takeDame();
                Destroy(gameObject);
            }
        }
    }
}
