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
        private static T instace;
        public static T Instance
        {
            get
            {
                if(instace == null)
                {
                    if(FindObjectOfType<T>() != null) // Nếu tìm thấy gameObject có Type T sẽ gán instance = gameObject được tìm thấy
                    {
                        instace = FindObjectOfType<T>();
                    }
                    else
                    {
                        new GameObject().AddComponent<T>(); // nếu không tìm thấy thì khởi tạo gameObject và tự động thêm class T vào gameObject
                    }
                }
                return instace;
            }
        }

        protected virtual void Awake()
        {
            if(instace != null && instace.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
            {
                Debug.LogError("Singleton already exist: " + instace.gameObject.name);
                DestroyImmediate(this.gameObject);
            }
            instace = this.GetComponent<T>();
        }
    }
}
