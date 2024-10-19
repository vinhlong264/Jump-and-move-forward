using TMPro;
using UnityEngine;

public class UI_Health : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI healthText;
    private void OnEnable()
    {
        Observer.Instance.addObserver(ActionType.Health, this);
    }

    private void OnDestroy()
    {
        Observer.Instance.removeObserver(ActionType.Health, this);
    }
    private void Start()
    {
        healthText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Notify(int _value)
    {
        healthText.text = _value.ToString();
    }
}
