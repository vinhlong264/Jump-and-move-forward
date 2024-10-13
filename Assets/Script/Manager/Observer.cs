﻿using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    static Dictionary<ActionType,List<IObserver>> observers = new Dictionary<ActionType, List<IObserver>>();

    public static void addObserver(ActionType type , IObserver o)
    {
        if (!observers.ContainsKey(type))
        {
            observers.Add(type, new List<IObserver>());
            observers[type].Add(o);
        }
    }

    public static void removeObserver( ActionType type ,IObserver o)
    {
        if (!observers.ContainsKey(type)) return;

        observers[type].Remove(o);
    }

    public static void Notify(ActionType type)
    {
        foreach(var o  in observers[type])
        {
            o.Notify();
        }
    }
}


public enum ActionType
{
    Question,
    Health,
    Score
}
