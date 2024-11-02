using System.Collections;
using UnityEngine;

public class FinishBase : MonoBehaviour
{
    [SerializeField] protected string nameScene;
    [SerializeField] protected string sceneLevel;

    protected SpriteRenderer sr;
    [SerializeField] protected ItemSO ItemSO;


    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        setUp(ItemSO);
    }
    protected virtual void setUp(ItemSO _itemSO)
    {
        sr.sprite = _itemSO.sr;
    }

    protected void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = ItemSO.sr;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }

    protected virtual void savePoint(Point _point)
    {

    }

    protected virtual void nextLevel(string _nameScene, string _sceneLevel)
    {
        StartCoroutine(loadScene(_nameScene, _sceneLevel));
    }

    protected IEnumerator loadScene(string _nameScene, string _sceneLevel)
    {
        Observer.Instance.Notify(ActionType.LoadScene, 0);
        yield return new WaitForSeconds(1);
        GameManager.Instance.LoadScene(_nameScene, _sceneLevel);
    }
}

[System.Serializable]
public class Point
{
    public float point_1;
    public float point_2;
    public float point_3;

    public float sumPoint(float x, float y, float z)
    {
        return (x + y + z) / 3;
    }
}
