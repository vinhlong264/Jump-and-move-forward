using UnityEngine;

public class Learn_3 : FinishBase
{
    [SerializeField] private GameObject UI_YouWIn;
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<CharacterStatus>() != null)
        {
            savePoint(GameManager.Instance.pointManager);
            GameManager.Instance.saveScore();
            Debug.Log("Hoàn thành khóa học");
            
            if(UI_YouWIn != null)
            {
                UI_YouWIn.SetActive(true);
            }

            CharacterStatus.Instance.noJump = true;
        }
    }

    protected override void savePoint(Point _point)
    {
        _point.point_3 = GameManager.Instance.score;
    }
}
