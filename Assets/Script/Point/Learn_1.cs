using System.Collections;
using UnityEngine;

public class Learn_1 : FinishBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>() != null)
        {
            savePoint(GameManager.Instance.pointManager);
            GameManager.Instance.winLevel();
            GameManager.Instance.addItem(ItemSO);
            nextLevel(nameScene, sceneLevel);
        }
    }

    protected override void savePoint(Point _point)
    {
        _point.point_1 = GameManager.Instance.score;
        GameManager.Instance.resetScore();
    }
}
