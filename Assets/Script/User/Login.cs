using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TMP_InputField password;

    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/UserData.txt";
        Debug.Log(File.Exists(filePath));
    }

    public void LoginUser()
    {
        if (!isFileExist(filePath)) return;

        UserData user = new UserData();

        user.username = userName.text;
        user.password = password.text;

        if (isValidUser(user.username, user.password))
        {
            Debug.Log("Start");
            StartCoroutine(loadScene());
        }
        else
        {
            Debug.Log("Thông tin tài khoản không chính xác");
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

    private bool isValidUser(string _userName, string _password)
    {
        if (!isFileExist(filePath)) return false;

        string[] data = File.ReadAllLines(filePath);
        foreach (var x in data)
        {
            string[] cols = x.Split(' ');
            if (_userName == cols[0] && _password == cols[1])
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator loadScene()
    {
        Observer.Instance.Notify(ActionType.LoadScene ,0);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }

}
