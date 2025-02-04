using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private QuestionSystem questionSystem;
    private void Start()
    {
        
    }

    private void eventActiveMap()
    {
        questionSystem.setActive();
        gameObject.SetActive(false);
    }
}
