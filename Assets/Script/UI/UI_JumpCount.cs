﻿using TMPro;
using UnityEngine;

public class UI_JumpCount : MonoBehaviour
{
    private TextMeshProUGUI jumpCountText;

    private void OnEnable()
    {
        Observer.Instance.addObserver(ActionType.JumpCount, Notify);
    }

    private void OnDisable()
    {
        Observer.Instance.removeObserver(ActionType.JumpCount, Notify);
    }

    private void Start()
    {
        jumpCountText = GetComponentInChildren<TextMeshProUGUI>();
        Notify(0);
    }
    public void Notify(int _value)
    {
        jumpCountText.text = _value.ToString();
    }
}
