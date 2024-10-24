using Extension;
using System;
using System.Collections.Generic;
using UnityEngine;
public class Observer : Singleton<Observer>
{
    public Dictionary<ActionType, List<Action<int>>> observers = new Dictionary<ActionType, List<Action<int>>>();
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Debug.Log(observers.Count);
    }
    public void addObserver(ActionType key, Action<int> callBack) // đăng kí sự kiện
    {
        if (!observers.ContainsKey(key))
        {
            Debug.Log("Đăng kí sự kiện");
            observers.Add(key, new List<Action<int>>());
        }

        observers[key].Add(callBack);
    }

    public void removeObserver(ActionType type, Action<int> callBack) // Hủy sự kiện
    {
        if (!observers.ContainsKey(type)) return;

        observers[type].Remove(callBack);
        Debug.Log("Hủy sự kiện");
    }

    public void Notify(ActionType key, int value) // Thông báo sự kiện
    {
        if (!observers.ContainsKey(key)) return;

        foreach (var callBack in observers[key])
        {
            try
            {
                callBack?.Invoke(value);
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat($"Notify action on key {key} error: {e.ToString()}");
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
