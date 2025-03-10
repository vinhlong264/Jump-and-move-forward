﻿using Extension;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>, ISaveManager
{
    public float score; // Score
    private string filepath;
    public float ScoreFinal;

    [Header("Save game")]
    [SerializeField] private int levelGame;
    public Point pointManager; // Quản lý Score
    public List<ItemSO> listItems = new List<ItemSO>();
    public List<float> listScore = new List<float>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
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

    public int currentLevel() => levelGame;

    public void saveScore()
    {
        ScoreFinal = pointManager.sumPoint(pointManager.point_1, pointManager.point_2, pointManager.point_3);

        listScore.Add(ScoreFinal);
    }

    public void addItem(ItemSO _item)
    {
        listItems.Add(_item);
    }

    public void LoadGame(GameData _data)
    {
        //Gán lại dữ liệu từ phía DataBase
        this.levelGame = _data.levelGame;
        this.pointManager = _data.point;

        foreach (var socre in _data.listScoreData)
        {
            listScore.Add(socre);
        }


        foreach (var item in _data.badgeList)
        {
            foreach (var j in GetItemDataBase())
            {
                if (j != null && j.itemID == item)
                {
                    listItems.Add(j);
                }
            }
        }
    }

    public void SaveGame(ref GameData _data)
    {
        _data.levelGame = this.levelGame;
        Debug.Log(_data.levelGame);

        _data.point = this.pointManager;

        if(_data.listScoreData.Count > 0)
        {
            _data.listScoreData.Clear();
        }

        foreach (var score in this.listScore)
        {
            _data.listScoreData.Add(score);
        }


        if(_data.badgeList.Count > 0)
        {
            _data.badgeList.Clear(); // xóa đi những phần tử cũ
        }

        foreach (var item in listItems)
        {
            _data.badgeList.Add(item.itemID);
        }
    }

    private List<ItemSO> GetItemDataBase()
    {
        List<ItemSO> itemDataBase = new List<ItemSO>(); // Khởi tạo List mới để lưu trữ các ItemSo
                                                        //#if UNITY_EDITOR
                                                        //        string[] assetName = AssetDatabase.FindAssets("", new[] { "Assets/DataSO/item" }); // lấy ra dữ liệu bên trong tệp theo địa chỉ
                                                        //        foreach (string SOname in assetName)
                                                        //        {
                                                        //            var SOpath = AssetDatabase.GUIDToAssetPath(SOname); // Lấy ra GUID từ đường dẫn
                                                        //            var itemData = AssetDatabase.LoadAssetAtPath<ItemSO>(SOpath); // Convert lại qua ItemSo để add vào List
                                                        //            itemDataBase.Add(itemData);
                                                        //        }
                                                        //#endif

        DataBaseSO dataBaseSO = Resources.Load<DataBaseSO>("DataBase");
        foreach (var item in dataBaseSO.DataBase)
        {
            itemDataBase.Add(item);
        }

        return itemDataBase;
    }
}

