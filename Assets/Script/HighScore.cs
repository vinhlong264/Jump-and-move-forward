using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private string filepath;
    [SerializeField] GameObject ScoreLine;
    [SerializeField] Transform scoreParent;
    [SerializeField] List<ScoreData> listScore = new List<ScoreData>();

    void Start()
    {
        filepath = Application.persistentDataPath + "/DataScore.txt";
        string[] scoreData = File.ReadAllLines(filepath);

        for(int i = 0; i < scoreData.Length; i++)
        {
            Debug.Log(scoreData[i]);
        }


        foreach(Transform child in scoreParent)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < scoreData.Length; i++)
        {
            ScoreData newScore = new ScoreData();
            newScore.rank = i + 1;
            newScore.score = float.Parse(scoreData[i]);
            listScore.Add(newScore);
        }

        if(listScore.Count > 0)
        {
            listScore = listScore.OrderByDescending(x => x.score).Take(3).ToList();
            for(int i = 0; i < listScore.Count; i++)
            {
                listScore[i].rank = i + 1;
                GameObject Score = Instantiate(ScoreLine, scoreParent);
                Score.GetComponent<ScoreController>().setScore(listScore[i]);
            }
        }


    }
}
