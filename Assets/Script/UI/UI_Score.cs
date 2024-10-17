using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = GameManager.Instance.score.ToString();
    }

    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = GameManager.Instance.score.ToString();
        }
    }
}
