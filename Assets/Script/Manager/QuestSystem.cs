using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour , IObserver
{
    [SerializeField] private QuestionSO questionData;
    [SerializeField] private List<Question> listQuestion;
    [SerializeField] private Question currentQuestion;

    [SerializeField] private TextMeshProUGUI descriptionText;

    public GameObject test;


    private ActionType actionType => ActionType.Question;

    private void Start()
    {
        gameObject.SetActive(false);

        Observer.addObserver(actionType, this);
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
        test.SetActive(true);
        StartCoroutine(transitionNextQuestion());
        gameObject.SetActive(false);
    }

    public void Notify()
    {
        gameObject.SetActive(true);
    }
}
