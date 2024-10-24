using Extension;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : Singleton<SaveManager> 
{
    private GameData gameData;
    private List<ISaveManager> saveManagers; // List các Data cần lưu
    private FileDataHander dataHander; // lưu trữ vào file
    [SerializeField] private string fileName = "GameData.json"; // tên file
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
        gameData = new GameData(); // Khởi tạo dữ liệu game khi người chơi mới tải game
    }

    public void LoadData()
    {
        gameData = dataHander.loadGame();

        if (gameData == null) // Nếu không có dữ liệu sẽ load game với dữ liệu rỗng
        {
            Debug.Log("No save data found!");
            NewGame();
        }

        //Load data nếu có dữ liệu
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadGame(gameData);
        }
    }

    public void SaveGame() // Lưu dữ liệu game
    {
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveGame(ref gameData);
        }

        dataHander.SaveGame(gameData);

        Debug.Log("Save finish");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManager()
    {
        IEnumerable<ISaveManager> saveManager = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>(); // Tìm tất cả các Object có loại MonoBehaviour và ISaveManager

        return new List<ISaveManager>(saveManager);
    }

}
