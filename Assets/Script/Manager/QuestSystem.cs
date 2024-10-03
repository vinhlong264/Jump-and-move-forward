using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem Intace;
    public List<QuestInfor> questInfors = new List<QuestInfor>();
    public QuestInfor qs = new QuestInfor();

    public TextMeshProUGUI contentText;
    private void Awake()
    {
        if (Intace != null)
        {
            Destroy(Intace);
        }
        else
        {
            Intace = this;
            DontDestroyOnLoad(this);
        }
    }


    private void Start()
    {
        setQuest();

        contentText.text = qs.content;
    }

    void setQuest()
    {
        qs = questInfors[0];
    }

    public void userTrue()
    {
        if (qs.isTrue)
        {
            Debug.Log("Correct!!");
        }
        else
        {
            Debug.Log("Wrong!!");
        }
    }


    public void userFalse()
    {
        if (!qs.isTrue)
        {
            Debug.Log("Correct!!");
        }
        else
        {
            Debug.Log("Wrong!!");
        }
    }



}

[System.Serializable]
public class QuestInfor
{
    public int id;
    public string content;
    public bool isTrue;
}
