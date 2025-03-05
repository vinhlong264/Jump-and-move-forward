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
    [SerializeField] private TextMeshProUGUI status;
    [SerializeField] private bool isLogin;

    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/GameData.json";

        Debug.Log(Application.persistentDataPath);
    }

    private void OnDisable()
    {
        status.text = "";
    }

    public void userLogin()
    {
        UserData user = new UserData();
        //user = GameManager.Instance.user;

        user.username = userName.text;
        user.password = password.text;

        if (!File.Exists(filePath))
        {
            Debug.Log("File không tồn tại");
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            status.text = "Vui lòng không để các trường bị trống";
        }

        string dataStore = File.ReadAllText(filePath);

        GameData gameData = JsonUtility.FromJson<GameData>(dataStore);
        Debug.Log(user.username + " - " + user.password);

        foreach (var x in gameData.allUsers)
        {
            if (x.username.ToLower() == user.username.ToLower() && x.password == user.password)
            {
                isLogin = true;
                status.text = "Login finish";
                break;
            }
        }

        if (isLogin)
        {
            DataOnly.UserName = user.username;
            Debug.Log(DataOnly.UserName);
            Debug.Log("Đăng nhập thành công");
            Observer.Instance.Notify(ActionType.LoadScene, 0);
            StartCoroutine(loadScene());
        }
        else
        {
            status.text = "Incorrect account information";
        }
    }


    IEnumerator loadScene()
    {
        Observer.Instance.Notify(ActionType.LoadScene, 0);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }


}
