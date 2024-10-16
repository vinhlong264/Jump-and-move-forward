using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Extension;
public class GameManager : Singleton<GameManager>
{
    public float score;
    public Point pointManager;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(Instance);
    }
    public void addScore()
    {
        score += 0.4f;
    }

    public void resetScore()
    {
        score = 0;
    }

    public void LoadScene(string _nameScene, string _sceneLevel)
    {
        SceneManager.LoadScene($"{_nameScene} {_sceneLevel}");
    }
}

