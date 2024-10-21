using System.IO;
using TMPro;
using UnityEngine;

public class Register : MonoBehaviour
{
    [SerializeField] private TMP_InputField UserName;
    [SerializeField] private TMP_InputField Password;

    private string filepath;

    private int maxCharacter = 16;
    private int minCharacter = 0;

    private void Start()
    {
        filepath = Application.persistentDataPath + "/UserData.txt";
    }

    public void RegisterUser()
    {
        UserData userData = new UserData();

        userData.username = UserName.text;
        userData.password = Password.text;


        if (string.IsNullOrEmpty(userData.username) || string.IsNullOrEmpty(userData.password))
        {
            Debug.Log("Vui lòng điền đầy đủ các trường");
            return;
        }

        if((userData.username.Length < maxCharacter && userData.password.Length < maxCharacter)
            || (userData.username.Length > minCharacter && userData.password.Length > minCharacter))
        {
            Debug.Log("Vượt quá kí tự cho phép");
            return;
        }

        if (isUserExists(userData.username))
        {
            Debug.Log("tên người dùng đã tồn tại");
            return;
        }

        if (isFileExist(filepath))
        {
            using(StreamWriter sw = new StreamWriter(filepath,true))
            {
                sw.WriteLine($"{userData.username} {userData.password}");
            }

            Debug.Log("Đăng kí thành công");
        }
    }

    private bool isFileExist(string path)
    {
        if (!File.Exists(path))
        {
            return false;
        }
        return true;
    }

    private bool isUserExists(string _userName)
    {
        if (!isFileExist(filepath)) return false;

        string[] data = File.ReadAllLines(filepath);
        foreach(var x in data)
        {
            string[] cols = x.Split(' ');
            if (_userName == cols[0])
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class UserData
{
    public string username;
    public string password;
}

