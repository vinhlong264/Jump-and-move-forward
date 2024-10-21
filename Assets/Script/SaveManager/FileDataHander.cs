using System.IO;
using UnityEngine;

public class FileDataHander
{
    private string dataDirPath;
    private string dataFileName;

    public FileDataHander(string _dataDirPath, string _dataFileName)
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
    }

    public void SaveGame(GameData _gameData)
    {
        string fullPath = Path.Combine(dataDirPath,dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(_gameData, true);
            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                Debug.Log("Tạo file thành công");
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

        GameData loadData = null;

        try
        {
            if (File.Exists(fullPath))
            {
                string dataToLoad = "";
                using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        dataToLoad = sr.ReadToEnd();
                    }
                }

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("No file found: "+fullPath+"\n"+e.ToString());
        }


        return loadData;
    }
}
