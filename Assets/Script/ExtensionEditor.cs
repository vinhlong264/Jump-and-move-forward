using UnityEngine;

namespace Extension
{
    public static class ExtensionEditor
    {
        public static bool DotDirectionTo(this Transform transform, Transform otherTransform, Vector2 direction) 
        {
            Vector2 getDirection = otherTransform.position - transform.position; // Lấy ra hướng của this tới transfomr của object cần so sánh
            return Vector2.Dot(direction.normalized, getDirection) > 0.25f; // Lấy ra Dot của hướng được lấy ra với hướng cần tính toán
        }
    }

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour // Singleton Improve
    {
        private static T _instace;
        public static T Instance
        {
            get
            {
                if(_instace == null)
                {
                    if(FindObjectOfType<T>() != null) // Nếu tìm thấy gameObject có Type T sẽ gán instance = gameObject được tìm thấy
                    {
                        _instace = FindObjectOfType<T>();
                    }
                    else
                    {
                        new GameObject().AddComponent<T>().name = $"{typeof(T)}"; // nếu không tìm thấy thì khởi tạo gameObject và tự động thêm class T vào gameObject
                    }
                }
                return _instace;
            }
        }

        protected virtual void Awake()
        {
            if(_instace != null && _instace.GetInstanceID() != this.GetInstanceID())
            {
                Debug.LogError("Singleton already exist: " + _instace.gameObject.name);
                DestroyImmediate(this.gameObject);
            }
            else
                _instace = this.GetComponent<T>();
        }
    }
}
