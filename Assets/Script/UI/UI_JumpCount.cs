using TMPro;
using UnityEngine;

public class UI_JumpCount : MonoBehaviour, IObserver
{
    private TextMeshProUGUI jumpCountText;

    private void OnEnable()
    {
        Observer.addObserver(ActionType.JumpCount, this);
    }

    private void OnDestroy()
    {
        Observer.removeObserver(ActionType.JumpCount, this);
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
