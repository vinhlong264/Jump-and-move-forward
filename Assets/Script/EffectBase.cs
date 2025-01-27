using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    protected Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void DestroySelf()
    {
        gameObject.SetActive(false);
    }
}
