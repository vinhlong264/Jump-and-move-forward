using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_YouWinLose : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreFinal;
    void Start()
    {
        if (scoreFinal == null) return;

        scoreFinal.text = GameManager.Instance.ScoreFinal.ToString("f2");
    }

    public void loadMenu()
    {
        StartCoroutine(load());
    }

    IEnumerator load()
    {
        Observer.Instance.Notify(ActionType.LoadScene, 0);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
