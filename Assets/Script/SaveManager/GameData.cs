using System.Collections.Generic;

[System.Serializable]
public class GameData // Class dùng để lưu trữ dữ liệu game level và các badge
{
    public List<UserData> allUsers;

    public GameData()
    {
        allUsers = new List<UserData>();
    }
}

[System.Serializable]
public class UserData
{
    public string username;
    public string password;

    public int levelGame; // Level game
    public List<float> listScoreData; // Score
    public List<string> badgeList;  // List dùng để lưu trữ các GUID(Globally Unique Identifier), ID sẽ được gán duy nhất cho từng phần từ trong Assets

    public UserData()
    {
        levelGame = 0;
        listScoreData = new List<float>();
        badgeList = new List<string>();
    }
}

