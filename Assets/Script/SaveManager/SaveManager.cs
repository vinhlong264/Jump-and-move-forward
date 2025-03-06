using Extension;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private List<ISaveManager> saveManagers; // List các Data cần lưu
    private FileDataHander dataHander; // lưu trữ vào file
    private string fileName = "GameData.json"; // tên file
    private GameData _gameData;


    public FileDataHander DataHander => dataHander;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(instace);
    }


    private void Start()
    {
        dataHander = new FileDataHander(Application.persistentDataPath, fileName);// Constructor gồm Directory và file name

        saveManagers = FindAllSaveManager();

        LoadData();
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void LoadData()
    {
        _gameData = dataHander.loadGame();

        if (_gameData == null) // Nếu không có dữ liệu thì tạo game mới
        {
            NewGame();
        }


        foreach (ISaveManager manager in saveManagers)
        {
            manager.LoadGame(_gameData);
        }

    }

    public void SaveData()
    {
        GameManager.instace.SaveGame( ref _gameData);
        dataHander.SaveGame(_gameData);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }


    private List<ISaveManager> FindAllSaveManager()
    {
        IEnumerable<ISaveManager> saveManager = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>(); // Tìm tất cả các Object có loại MonoBehaviour và ISaveManager

        return new List<ISaveManager>(saveManager);
    }

}
