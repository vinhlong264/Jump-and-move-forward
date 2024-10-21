using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badge : MonoBehaviour
{
    [SerializeField] private UI_Badge[] uiBadge;
    [SerializeField] private Transform UI_BadgeParent;

    void Start()
    {
        uiBadge = UI_BadgeParent.GetComponentsInChildren<UI_Badge>();

    }

    private void Update()
    {
        if(GameManager.Instance.listItems.Count <= 0) return;


        if(GameManager.Instance.listItems.Count <= 1)
        {
            uiBadge[0].setUp(GameManager.Instance.listItems[0]);
        }
        else
        {
            for(int i = 0; i <  uiBadge.Length; i++)
            {
                uiBadge[i].setUp(GameManager.Instance.listItems[i]); 
            }
        }
    }
}
