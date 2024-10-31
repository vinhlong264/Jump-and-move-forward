using System;
using System.Collections.Generic;
using UnityEngine;
using Extension;
public class Observer : Singleton<Observer>
{

    protected override void Awake()
    {
        base.Awake();
    }

    public  Dictionary<ActionType, Action<int>> observers = new Dictionary<ActionType, Action<int>>();
    public void addObserver(ActionType key, Action<int> callBack) // đăng kí sự kiện
    {
        if (!observers.ContainsKey(key)) // Nếu key không tồn tại thì add thêm 1 key và callback
        {
            Debug.Log("Đăng kí sự kiện");
            observers.Add(key, callBack);
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
        foreach (KeyValuePair<ActionType, Action<int>> pair in new Dictionary<ActionType, Action<int>>(observers))
        {
            if (pair.Key.Equals(key))
            {
                pair.Value?.Invoke(value);
            }
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
