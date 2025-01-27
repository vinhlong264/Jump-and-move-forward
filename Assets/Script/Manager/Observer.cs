using Extension;
using System;
using System.Collections.Generic;
using UnityEngine;
public class Observer : Singleton<Observer>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public Dictionary<ActionType, List<Action<int>>> observers = new Dictionary<ActionType, List<Action<int>>>();
    public void addObserver(ActionType key, Action<int> callBack) // đăng kí sự kiện
    {
        if (observers.ContainsKey(key))
        {
            Debug.Log("Key tồn tại thêm vào sự kiện: " + callBack);
            if (observers[key].Count > 0)
            {
                observers[key].Add(callBack);
                return;
            }

            return;
        }

        Debug.Log("Key không tồn tại");

        observers.Add(key, new List<Action<int>>());
        observers[key].Add(callBack);
    }

    public void removeObserver(ActionType type, Action<int> callBack) // Hủy sự kiện
    {
        if (!observers.ContainsKey(type)) return;

        if (observers[type].Count > 0)
        {
            observers[type].Remove(callBack);
        }
        else
        {
            observers.Remove(type);
        }
    }

    public void Notify(ActionType key, int value) // Thông báo sự kiện
    {
        if (!observers.ContainsKey(key)) return;

        List<Action<int>> dump = observers[key];

        foreach (Action<int> action in dump)
        {
            action.Invoke(value);
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
