using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] protected GameObject[] listUI;
    protected virtual void Start()
    {
        if (listUI.Length <= 0) return;

        isActiveGameObj(listUI[0]);
    }


    public void isActiveGameObj(GameObject acticve)
    {
        for (int i = 0; i < listUI.Length; i++)
        {
            listUI[i].SetActive(false);
        }

        if(acticve != null)
        {
            acticve.SetActive(true);
        }
    }
}
