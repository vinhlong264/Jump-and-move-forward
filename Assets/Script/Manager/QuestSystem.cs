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
    [SerializeField] private GameObject container;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI notifyAnswer;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Animator animText;
    [SerializeField] private Animator animUIQuestion;

    [Header("Map")]
    [SerializeField] private int indexMap = 0;
    [SerializeField] private GameObject[] Map;
    [SerializeField] private ActionType actionType;

    [Header("Deadline")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;
    [SerializeField] private float timeDuration;
                     private float timeCheck;
                     private bool isDeadLine;


    private void OnEnable()
    {
        Observer.Instance.addObserver(actionType, Notify);
    }

    private void OnDisable()
    {
        Observer.Instance.removeObserver(actionType, Notify);
    }

    private void Start()
    {
        container.SetActive(false);
        listQuestion = questionData.informationQuestion.ToList();
        timeCheck = timeDuration;
        timeText.text = timeCheck.ToString();


        setQuestion();
    }



    private void Update()
    {
        if (isDeadLine)
        {
            timeCheck -= Time.deltaTime;
            if(timeCheck <= 0)
            {
                timeCheck = timeDuration;
                GameObject newBullet = Instantiate(bullet, firePos.position, Quaternion.identity);
                if(newBullet != null)
                {
                    newBullet.GetComponent<Angry_bullet>().AttackTarget(CharacterStatus.Instance.transform, 5f);
                }

                Debug.Log("Quá thời hạn");
            }
            else
            {
                timeText.text = timeCheck.ToString("f2");
            }
        }
        else
        {
            timeCheck = timeDuration;
        }
    }



    private void setQuestion()
    {
        int randomIndex = Random.Range(0, listQuestion.Count); // Đảo câu hỏi 1 cách ngẫu nhiên
        currentQuestion = listQuestion[randomIndex]; // Lấy ra câu hỏi được chọn
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
            //timeCheck = timeDuration;
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
            //timeCheck = timeDuration;
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

    private void setActive() // Quản lý việc mở khóa map tiếp theo
    {
        CharacterStatus.Instance.noJump = false;
        activeMap();
        StartCoroutine("transitionNextQuestion");
        container.SetActive(false);
    }

    private void activeMap()
    {
        Map[indexMap].SetActive(true);
        indexMap++;
    }

    public void Notify(int value)
    {
        container.SetActive(true);
        CharacterStatus.Instance.noJump = true;
        isDeadLine = true;
    }
}
