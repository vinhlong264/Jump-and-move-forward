using UnityEngine;
using UnityEngine.UI;

public class UI_Badge : MonoBehaviour
{
    private ItemSO ItemSO;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.clear;
    }

    public void setUp(ItemSO itemSO)
    {
        if (itemSO != null)
        {
            image.sprite = itemSO.sr;
            image.color = Color.white;
        }
    }


}
