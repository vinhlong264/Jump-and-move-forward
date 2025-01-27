using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] selectLevelBtn;

    private void Start()
    {
        for (int i = 0; i < selectLevelBtn.Length; i++)
        {
            selectLevelBtn[i].interactable = false;
        }

        for(int i = 0; i <= GameManager.Instance.currentLevel(); i++)
        {
            selectLevelBtn[i].interactable = true;
        }
    }


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
