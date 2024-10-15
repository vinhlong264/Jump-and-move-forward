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

    [SerializeField] private TextMeshProUGUI notifyAnswer;
    [SerializeField] private Animator animText;
    [SerializeField] private Animator animUIQuestion;

    [Header("Game object")]
    [SerializeField] private int indexMap = 0;
    [SerializeField] private GameObject[] Map;





    [SerializeField] private ActionType actionType;


    private void OnEnable()
    {
        Observer.addObserver(actionType, this);
    }

    private void Start()
    {
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

        notifyAnswer.text = "";
    }

    public void useSelectTrue()
    {

        if (currentQuestion.isTrue)
        {
            notifyAnswer.text = "Correct!!";
        }
        else
        {
            notifyAnswer.text = "Wrong!!";
        }

        animText.SetTrigger("transition");
        Invoke("completeQuestion", 2f);

    }

    public void useSelectFalse()
    {
        if (!currentQuestion.isTrue)
        {
            notifyAnswer.text = "Correct!!";
        }
        else
        {
            notifyAnswer.text = "Wrong!!";
        }

        animText.SetTrigger("transition");
        Invoke("completeQuestion", 2f);

    }

    private void completeQuestion()
    {
        animUIQuestion.SetTrigger("UItransition");
    }

    IEnumerator transitionNextQuestion()
    {
        listQuestion.Remove(currentQuestion);
        setQuestion();
        yield return new WaitForSeconds(1);
    }

    private void setActive()
    {
        activeMap();
        StartCoroutine("transitionNextQuestion");
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
