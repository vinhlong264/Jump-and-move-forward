using TMPro;
using UnityEngine;

public class UI_Health : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    private void OnEnable()
    {
        Observer.Instance.addObserver(ActionType.Health, Notify);
    }

    private void OnDisable()
    {
        Observer.Instance.removeObserver(ActionType.Health, Notify);
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
