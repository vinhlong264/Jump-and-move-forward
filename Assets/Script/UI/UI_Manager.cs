using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] protected GameObject[] listUI;
    protected virtual void Start()
    {
        if (listUI.Length <= 0) return;

        isEnableGameObj(listUI[0]);
    }

    public void isEnableGameObj(GameObject acticve)
    {
        for (int i = 0; i < listUI.Length; i++)
        {
            listUI[i].SetActive(false);
        }

        if (acticve != null)
        {
            acticve.SetActive(true);
        }
    }
    public void logOut()
    {
        StartCoroutine(loadScene("MenuFirst"));
    }

    IEnumerator loadScene(string nameScene)
    {
        Observer.Instance.Notify(ActionType.LoadScene, 0);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nameScene);
    }

    public void playBtnHandler()
    {
        StartCoroutine(loadScene("Menu"));
    }

}
