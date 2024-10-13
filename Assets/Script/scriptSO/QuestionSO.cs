using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestData" , menuName = "Question/QuestionInfor")]
public class QuestionSO : ScriptableObject
{
    public string nameQuest;
    public Question[] informationQuestion;
}

[System.Serializable]
public class Question
{
    public string Description;
    public bool isTrue;
    public int point;
}

