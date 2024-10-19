using Extension;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public float score;
    public Point pointManager;
    private string filepath;

    public List<ItemSO> listItems = new List<ItemSO>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        filepath = Application.persistentDataPath + "/DataScore.txt";
        Debug.Log(File.Exists(filepath));
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

    public void saveScore()
    {
        if (File.Exists(filepath))
        {
            float ScoreFinal = pointManager.sumPoint(pointManager.point_1,pointManager.point_2,pointManager.point_3);

            using(StreamWriter writer = new StreamWriter(filepath,true))
            {
                writer.Write(ScoreFinal);
            }

            Debug.Log("Ghi file thành công");
        }
    }

    public void addItem(ItemSO _item)
    {
        listItems.Add(_item);
    }
}

