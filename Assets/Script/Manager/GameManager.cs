using Extension;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>, ISaveManager
{
    public float score;
    public Point pointManager;
    private string filepath;
    public float ScoreFinal;


    [Header("Save game")]
    [SerializeField] private int levelGame;
    public List<ItemSO> listItems = new List<ItemSO>();



    [Header("DataBase")]
    public List<ItemSO> loadItemDatabase;


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

    public int checkLevelGame() => levelGame;

    public void winLevel()
    {
        levelGame++;
    }

    public void saveScore()
    {
        if (File.Exists(filepath))
        {
            ScoreFinal = pointManager.sumPoint(pointManager.point_1, pointManager.point_2, pointManager.point_3);

            using (StreamWriter writer = new StreamWriter(filepath, true))
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

    public void LoadGame(GameData _data)
    {
        _data.levelGame = this.levelGame;

        foreach(var item in _data.badgeList)
        {
            foreach(var i in GetItemDataBase())
            {
                if(i != null && i.itemID == item)
                {
                    listItems.Add(i);
                }
            }
        }

    }

    public void SaveGame(ref GameData _data)
    {
        this.levelGame = _data.levelGame;

        _data.badgeList.Clear();

        foreach (var item in listItems)
        {
            _data.badgeList.Add(item.itemID);
        }

    }


    private List<ItemSO> GetItemDataBase()
    {
        List<ItemSO> itemDataBase = new List<ItemSO>();
        string[] assetName = AssetDatabase.FindAssets("", new[] { "Assets/DataSO/item" });
        foreach (string SOname in assetName)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOname);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemSO>(SOpath);
            itemDataBase.Add(itemData);
        }

        return itemDataBase;
    }
}

