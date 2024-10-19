using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankTxt;
    [SerializeField] private TextMeshProUGUI scoreTxt;

    public void setScore(int rank, float score)
    {
        rankTxt.text = rank.ToString();
        scoreTxt.text = score.ToString("f2");
    }
}
