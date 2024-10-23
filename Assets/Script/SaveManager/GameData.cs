using System.Collections.Generic;

[System.Serializable]
public class GameData // Class dùng để lưu trữ dữ liệu game level và các badge
{
    public int levelGame;
    public List<string> badgeList; // List dùng để lưu trữ các GUID(Globally Unique Identifier), ID sẽ được gán duy nhất cho từng phần từ trong Assets

    public GameData() // Constructor khi game chưa có dữ liệu gì cả
    {
        this.levelGame = 0;
        badgeList = new List<string>();
    }
}
