using UnityEngine;

public class UI_LoadScene : MonoBehaviour,IObserver
{
    private Animator anim;

    private void Awake()
    {
        Observer.addObserver(ActionType.LoadScene,this);
    }

    private void OnDestroy()
    {
        Observer.removeObserver(ActionType.LoadScene,this);
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
