using UnityEngine;

[CreateAssetMenu(fileName = "itemData", menuName = "ItemData/Iteminfor")]
public class ItemSO : ScriptableObject
{
    public Sprite sr;
    public string itemID; // Lưu trữ dữ GUID

    //    private void OnValidate()
    //    {
    //#if UNITY_EDITOR
    //        string path = AssetDatabase.GetAssetPath(this); // lấy ra đường dẫn của object trong Assets
    //        itemID = AssetDatabase.AssetPathToGUID(path); // lấy ra GUID từ path
    //#endif
    //    }
}
