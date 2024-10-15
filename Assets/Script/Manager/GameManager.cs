using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }
    public CharacterStatus character;

    public float score { get; set; }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    public void addScore()
    {
        score += 0.4f;
    }



}

