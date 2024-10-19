using System.Collections.Generic;
using UnityEngine;
using Extension;
public class Observer:Singleton<Observer>
{
    public Dictionary<ActionType,List<IObserver>> observers = new Dictionary<ActionType, List<IObserver>>();
    protected override void Awake()
    {
        base.Awake();
    }
    public void addObserver(ActionType type , IObserver o)
    {
        if (observers.ContainsKey(type)) return;


        if (!observers.ContainsKey(type))
        {
            Debug.Log("Đăng kí sự kiện");
            observers.Add(type, new List<IObserver>());
            observers[type].Add(o);
        }
    }

    public void removeObserver(ActionType type ,IObserver o)
    {
        if (!observers.ContainsKey(type)) return;

        observers[type].Remove(o);
        Debug.Log("Hủy sự kiện");
    }

    public void Notify(ActionType type , int value) // Thông báo sự kiện
    {
        if (!observers.ContainsKey(type)) return;
        
        foreach(var o  in observers[type])
        {
            o.Notify(value);
        }
    }
}


public enum ActionType
{
    Question,
    Health,
    JumpCount,
    LoadScene
}
