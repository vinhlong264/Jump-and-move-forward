using System.Collections.Generic;
using UnityEngine;
using Extension;
public class Observer
{
    public static Dictionary<ActionType,List<IObserver>> observers = new Dictionary<ActionType, List<IObserver>>();
    public static void addObserver(ActionType type , IObserver o)
    {
        if (observers.ContainsKey(type)) return;


        if (!observers.ContainsKey(type))
        {
            Debug.Log("Đăng kí sự kiện");
            observers.Add(type, new List<IObserver>());
            observers[type].Add(o);
        }
    }

    public static void removeObserver(ActionType type ,IObserver o)
    {
        if (!observers.ContainsKey(type))
        {
            Debug.Log("Key không tồn tại");
            return;
        }

        observers[type].Remove(o);
        Debug.Log("Hủy sự kiện");
    }

    public static void Notify(ActionType type , int value) // Thông báo sự kiện
    {
        if (!observers.ContainsKey(type)){
            Debug.Log("Không tìm thấy key");
            return;
        }
        
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
