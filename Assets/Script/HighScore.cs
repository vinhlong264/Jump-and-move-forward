using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private string filepath;
    [SerializeField] GameObject ScoreLine;
    [SerializeField] Transform scoreParent;
    [SerializeField] List<ScoreData> listHighScore = new List<ScoreData>();
    [SerializeField] List<float> scoreDump;

    void Start()
    {
        foreach(var x in GameManager.Instance.listScore)
        {
            scoreDump.Add(x);
        }

        Debug.Log(GameManager.Instance.listScore.Count);

        if (scoreDump.Count <= 0) return;


        for (int i = 0; i < scoreDump.Count; i++)
        {
            Debug.Log(scoreDump[i]);
        }


        foreach (Transform child in scoreParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < scoreDump.Count; i++)
        {
            ScoreData newScore = new ScoreData();
            newScore.rank = i + 1;
            newScore.score = scoreDump[i];
            listHighScore.Add(newScore);
        }

        if (listHighScore.Count > 0)
        {
            listHighScore = listHighScore.OrderByDescending(x => x.score).Take(3).ToList();
            for (int i = 0; i < listHighScore.Count; i++)
            {
                listHighScore[i].rank = i + 1;
                GameObject Score = Instantiate(ScoreLine, scoreParent);
                Score.GetComponent<ScoreController>().setScore(listHighScore[i]);
            }
        }


    }
}
