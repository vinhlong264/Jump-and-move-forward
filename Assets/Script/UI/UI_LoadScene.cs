using UnityEngine;

public class UI_LoadScene : MonoBehaviour
{
    private Animator anim;

    private void OnEnable()
    {
        Observer.Instance.addObserver(ActionType.LoadScene, Notify);
    }

    private void OnDisable()
    {
        Observer.Instance.removeObserver(ActionType.LoadScene, Notify);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Notify(int _value)
    {
        anim.SetTrigger("Load");
    }
}
