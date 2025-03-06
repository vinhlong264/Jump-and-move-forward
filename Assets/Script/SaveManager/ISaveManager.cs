public interface ISaveManager // Quản lý về việc lưu trữ dữ liệu
{
    void LoadGame(GameData _gameData); // Load data
    void SaveGame(ref GameData _gameData); // Save data
}
