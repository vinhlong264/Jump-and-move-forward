using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase" , menuName = "Data")]
public class DataBaseSO : ScriptableObject
{
    public List<ItemSO> DataBase = new List<ItemSO>();
}
