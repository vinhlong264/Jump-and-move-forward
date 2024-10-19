using UnityEngine;

public class UI_LoadScene : MonoBehaviour,IObserver
{
    private Animator anim;

    private void OnEnable()
    {
        Observer.Instance.addObserver(ActionType.LoadScene, this);
    }

    private void OnDestroy()
    {
        Observer.Instance.removeObserver(ActionType.LoadScene, this);
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
