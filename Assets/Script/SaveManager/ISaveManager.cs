public interface ISaveManager // Quản lý về việc lưu trữ dữ liệu
{
    void LoadGame(GameData _data); // Load data
    void SaveGame(ref GameData _data); // Save data
}
