using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private QuestionSO questionData; // Data
    [SerializeField] private List<Question> listQuestion; // Ds chứa câu hỏi
    [SerializeField] private Question currentQuestion; // Câu hỏi

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private TextMeshProUGUI notifyAnswer;
    [SerializeField] private Animator animText;
    [SerializeField] private Animator animUIQuestion;

    [Header("Map")]
    [SerializeField] private int indexMap = 0;
    [SerializeField] private GameObject[] Map;
    [SerializeField] private ActionType actionType;

    [Header("GameObejct")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float timeDuration;
    [SerializeField] private float timeCheck;
    [SerializeField] private bool isDeadLine;



    private void OnEnable()
    {
        Observer.Instance.addObserver(actionType, Notify);
    }

    private void OnDestroy()
    {
        Observer.Instance.removeObserver(actionType, Notify);
    }

    private void Start()
    {
        gameObject.SetActive(false);
        listQuestion = questionData.informationQuestion.ToList();
        timeCheck = timeDuration;
        setQuestion();
    }



    private void Update()
    {
        if (isDeadLine)
        {
            timeCheck -= Time.deltaTime;
            if(timeCheck <= 0)
            {
                isDeadLine = false;
                timeCheck = timeDuration;
                Debug.Log("Quá thời hạn");
            }
        }
    }



    private void setQuestion()
    {
        int randomIndex = Random.Range(0, listQuestion.Count); // Đảo câu hỏi 1 cách ngẫu nhiên
        currentQuestion = listQuestion[randomIndex];
        descriptionText.text = currentQuestion.Description;

        notifyAnswer.text = "";
    }

    public void useSelectTrue()
    {

        if (currentQuestion.isTrue)
        {
            notifyAnswer.text = "Correct!!";
            GameManager.Instance.score += currentQuestion.point;
            isDeadLine = false;
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
            GameManager.Instance.score += currentQuestion.point;
            isDeadLine = false;
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
        CharacterStatus.Instance.noJump = false;
        activeMap();
        StartCoroutine("transitionNextQuestion");
        gameObject.SetActive(false);
    }

    private void activeMap()
    {
        Map[indexMap].SetActive(true);
        indexMap++;
    }

    public void Notify(int value)
    {
        gameObject.SetActive(true);
        CharacterStatus.Instance.noJump = true;
        isDeadLine = true;
    }
}
