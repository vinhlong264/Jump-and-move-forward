using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankTxt;
    [SerializeField] private TextMeshProUGUI scoreTxt;

    public void setScore(ScoreData scoreData)
    {
        rankTxt.text = scoreData.rank.ToString();
        scoreTxt.text = scoreData.score.ToString("f2");
    }
}

[System.Serializable]
public class ScoreData
{
    public int rank;
    public float score;
}
