using UnityEngine;

public class Learn_3 : FinishBase
{
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
        }
    }

    protected override void savePoint(Point _point)
    {
        _point.point_3 = GameManager.Instance.score;
    }
}
