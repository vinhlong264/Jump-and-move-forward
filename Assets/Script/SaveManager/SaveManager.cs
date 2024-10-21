using Extension;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private FileDataHander dataHander;
    [SerializeField] private string fileName;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        dataHander= new FileDataHander(Application.persistentDataPath,fileName);

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
