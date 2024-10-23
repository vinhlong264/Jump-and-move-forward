using System.IO;
using UnityEngine;

public class FileDataHander
{
    private string dataDirPath;
    private string dataFileName;

    public FileDataHander(string _dataDirPath, string _dataFileName) // Constructor
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
    }

    public void SaveGame(GameData _gameData) // Save game
    {
        string fullPath = Path.Combine(dataDirPath,dataFileName); // Lấy ra path gồm đường dẫn và file name thông qua Path.Combine()

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(_gameData, true);
            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(dataToStore);
                }
            }

        }
        catch (System.Exception e)
        {
            Debug.LogError("Error trying on save data: " + fullPath + "\n" + e.ToString());
        }

    }

    public GameData loadGame()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData loadData = null; // tạo 1 GameData rỗng

        try
        {
            if (File.Exists(fullPath))// kiểm tra xem file có tồn tại không
            {
                string dataToLoad = ""; // lưu trữ dữ liệu vào biến này
                using (FileStream fs = new FileStream(fullPath, FileMode.Open)) // đọc tất cả dữ liệu trong file
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        dataToLoad = sr.ReadToEnd();
                    }
                }

                loadData = JsonUtility.FromJson<GameData>(dataToLoad); // convert dữ liệu thành GameData

            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("No file found: "+fullPath+"\n"+e.ToString());
        }


        return loadData;
    }
}
