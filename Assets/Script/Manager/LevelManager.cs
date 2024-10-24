using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public void selectLevel(string level)
    {
        StartCoroutine(loadScene(level));
    }

    IEnumerator loadScene(string _level)
    {
        Observer.Instance.Notify(ActionType.LoadScene, 0);
        yield return new WaitForSeconds(1);
        GameManager.Instance.LoadScene("Level", _level);
    }
}
