using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private string filepath;

    void Start()
    {
        filepath = Application.persistentDataPath + "/DataScore.txt";
    }

    // Update is called once per frame
    void Update()
    {
        string[] scoreData = File.ReadAllLines(filepath);
        List<string> listData = scoreData.ToList();
    }
}
