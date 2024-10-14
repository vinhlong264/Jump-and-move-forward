using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour , IObserver
{
    [Header("Data")]
    [SerializeField] private QuestionSO questionData;
    [SerializeField] private List<Question> listQuestion;
    [SerializeField] private Question currentQuestion;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Game object")]
    [SerializeField] private int indexMap = 0;
    [SerializeField] private GameObject[] Map;





    [SerializeField] private ActionType actionType;

    private void Start()
    {
        Observer.addObserver(actionType, this);
        gameObject.SetActive(false);
        listQuestion = questionData.informationQuestion.ToList();

        setQuestion();
    }

    private void OnDestroy()
    {
        Observer.removeObserver(actionType, this);
    }

    private void setQuestion()
    {
        int randomIndex = Random.Range(0, listQuestion.Count);
        currentQuestion = listQuestion[randomIndex];
        descriptionText.text = currentQuestion.Description;
    }

    public void useSelectTrue()
    {

        if (currentQuestion.isTrue)
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Wrong");
        }
        Invoke("setActive", 1f);
    }

    public void useSelectFalse()
    {
        if (!currentQuestion.isTrue)
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Wrong");
        }
        Invoke("setActive", 1f);

    }


    IEnumerator transitionNextQuestion()
    {
        setQuestion();
        yield return new WaitForSeconds(1);
    }

    private void setActive()
    {
        StartCoroutine(transitionNextQuestion());
        activeMap();
        gameObject.SetActive(false);
    }

    private void activeMap()
    {
        Map[indexMap].SetActive(true);
        indexMap++;
    }

    public void Notify()
    {
        gameObject.SetActive(true);
    }
}
