using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestData" , menuName = "Question/QuestionInfor")]
public class questSO : ScriptableObject
{
    public string nameQuest;
    public List<QuestItems> requirmentQuest = new List<QuestItems>();
}

[System.Serializable]
public class QuestItems
{
    public string Description;
    public bool isTrue;
    public int point;
}

