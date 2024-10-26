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
    private UserData _userData;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        dataHander = new FileDataHander(Application.persistentDataPath, fileName);// Constructor gồm Directory và file name

        saveManagers = FindAllSaveManager();

        LoadData();
    }

    public void NewGame()
    {
        _gameData = new GameData()
        {
            allUsers = new List<UserData>()
            {
                new UserData()
                {
                    levelGame = 0,
                    score = 0,
                    badgeList = new List<string>()
                }
            }
        }; // Khởi tạo dữ liệu game khi người chơi mới tải game
    }

    public void LoadData()
    {
        _gameData = dataHander.loadGame();

        var get = FindUserManager();

        if (get != null)
        {
            _userData = get;
        }

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadGame(_userData);
        }
    }

    public void SaveGame() // Lưu dữ liệu game
    {
        _userData = FindUserManager();

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveGame(ref _userData);
        }

        //_gameData.allUsers.Add(_userData);

        dataHander.SaveGame(_gameData);

        Debug.Log("Save Finish");

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private UserData FindUserManager()
    {
        return _gameData.allUsers.FirstOrDefault(x => x.username == DataOnly.UserName); // Lấy ra người chơi đăng nhập
    }


    private List<ISaveManager> FindAllSaveManager()
    {
        IEnumerable<ISaveManager> saveManager = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>(); // Tìm tất cả các Object có loại MonoBehaviour và ISaveManager

        return new List<ISaveManager>(saveManager);
    }

}
