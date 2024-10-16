using UnityEngine;

public class UI_LoadScene : MonoBehaviour,IObserver
{
    private Animator anim;

    private void Awake()
    {
        Observer.addObserver(ActionType.LoadScenen,this);
    }

    private void OnDestroy()
    {
        Observer.removeObserver(ActionType.LoadScenen,this);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Notify()
    {
        anim.SetTrigger("Load");
    }
}
