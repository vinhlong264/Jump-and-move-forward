using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Learn_2 : FinishBase
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
            GameManager.Instance.winLevel();
            nextLevel(nameScene,sceneLevel);
        }
    }

    protected override void savePoint(Point _point)
    {
        _point.point_2 = GameManager.Instance.score;
        GameManager.Instance.resetScore();
    }
}
