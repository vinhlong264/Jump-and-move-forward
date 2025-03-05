using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class ObjectPooling : Singleton<ObjectPooling>
{
    private Dictionary<GameObject , List<GameObject>> pools = new Dictionary<GameObject , List<GameObject>>();
    protected override void Awake()
    {
        base.Awake();
    }


    public GameObject GetObj(GameObject baseObj)
    {
        if (pools.ContainsKey(baseObj))
        {
            foreach (var getObj in pools[baseObj])
            {
                getObj.SetActive(true);
                return getObj;
            }
        }

        GameObject tmp = Instantiate(baseObj);
        pools.Add(baseObj, new List<GameObject>());
        pools[baseObj].Add(tmp);
        tmp.SetActive(true);
        return tmp;
    }
}
