using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestData" , menuName = "Quest/QuestInfor")]
public class questSO : ScriptableObject
{
    public string nameQuest;
    public string questDescription;
    public List<QuestItems> requirmentQuest = new List<QuestItems>();
}

[System.Serializable]
public class QuestItems
{
    public itemSO itemData;
    public int size;

    public QuestItems(itemSO _itemData)
    {
        itemData = _itemData;
        addStack();
    }


    public void addStack()
    {
        size++;
    }

    public void removeStack()
    {
        size--;
    }
}

