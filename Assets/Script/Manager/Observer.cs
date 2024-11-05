using System;
using System.Collections.Generic;
using UnityEngine;
using Extension;
using System.Linq;
public class Observer : Singleton<Observer>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public Dictionary<ActionType, Action<int>> observers = new Dictionary<ActionType, Action<int>>();
    public void addObserver(ActionType key, Action<int> callBack) // đăng kí sự kiện
    {
        if (!observers.ContainsKey(key))
        {
            observers[key] = callBack;
        }

        observers[key] += callBack;
    }

    public void removeObserver(ActionType type, Action<int> callBack) // Hủy sự kiện
    {
        if (!observers.ContainsKey(type)) return;

        observers[type] -= callBack;

        if (observers[type] == null)
        {
            observers.Remove(type);
        }

        Debug.Log("Hủy sự kiện");
    }

    public void Notify(ActionType key, int value) // Thông báo sự kiện
    {
        Dictionary<ActionType , Action<int>> dumpDictionary = new Dictionary<ActionType, Action<int>>(observers);

        if(dumpDictionary.TryGetValue(key , out var action))
        {
            action?.Invoke(value);
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
