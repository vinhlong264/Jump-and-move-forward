using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] listUI;
    void Start()
    {
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
