using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    static Dictionary<string,List<IObserver>> observers = new Dictionary<string, List<IObserver>>();

    public static void addObserver( string key , IObserver o)
    {
        if (!observers.ContainsKey(key))
        {
            observers.Add(key, new List<IObserver>());
            observers[key].Add(o);
        }
    }

    public static void removeObserver( string key ,IObserver o)
    {
        if (!observers.ContainsKey(key)) return;

        observers[key].Remove(o);
    }

    public static void Notify(string key)
    {
        foreach(var o  in observers[key])
        {
            o.Notify();
        }
    }
}
