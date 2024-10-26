public interface ISaveManager // Quản lý về việc lưu trữ dữ liệu
{
    void LoadGame(UserData _user); // Load data
    void SaveGame(ref UserData _user); // Save data
}
