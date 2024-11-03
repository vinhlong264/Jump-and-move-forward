using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    private string filePath;
    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TextMeshProUGUI Status;
    private bool isRegister;


    private int maxChacracter = 16;
    private int minChacracter = 0;
    private UserData user;
    private GameData gameData;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/GameData.json";
        user = new UserData();
    }

    public void userRegister()
    {
        user.username = userName.text;
        user.password = password.text;


        if(string.IsNullOrEmpty(user.username) || string.IsNullOrEmpty(user.password))
        {
            Status.text = "Please fill in all fields";
            return;
        }

        if((user.username.Length > maxChacracter && user.password.Length > maxChacracter)
            || (user.username.Length <= minChacracter && user.password.Length <= minChacracter))
        {
            Status.text = "Exceeded allowed characters";
            return;
        }




        try
        {
            if (!isValidUser(user.username)) // Kiểm tra UserName có đang tồn tại không
            {
                Status.text = "Username already exists";
                return;
            }

            if (File.Exists(filePath))
            {
                //Nếu file đã tồn tại, thì sẽ đọc và convert thành gameData
                string jsonData = File.ReadAllText(filePath);
                gameData = JsonUtility.FromJson<GameData>(jsonData) ?? new GameData();
            }
            else
            {
               //Nếu không thì tạo 1 gameData mới
                gameData = new GameData();
            }

            //Thêm dữ liệu mới vào
            gameData.allUsers.Add(user);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            string dataStore = JsonUtility.ToJson(gameData,true);

            using(FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    isRegister = true;
                    sw.Write(dataStore);
                }
            }
            Status.text = "Register Finish";
        }
        catch(System.Exception e)
        {
            Debug.LogError("No trying on" + e.ToString() + ",Exception:" + filePath);
        }


        if (isRegister)
        {
            DataOnly.UserName = user.username;
            Debug.Log(DataOnly.UserName);
            StartCoroutine(loadScene());
        }


    }

    IEnumerator loadScene()
    {
        Observer.Instance.Notify(ActionType.LoadScene, 0);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }


    private bool isValidUser(string username)
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("File không tồn tại");
            return false;
        }

        string loadToFile = File.ReadAllText(filePath);
        Debug.Log("LoadToFile: "+string.Join("\n", loadToFile));

        GameData gameData = JsonUtility.FromJson<GameData>(loadToFile);
        
        for(int i = 0; i < gameData.allUsers.Count; i++)
        {
            if(username == gameData.allUsers[i].username)
            {
                return false;
            }
        }

        return true;
    }

}


