using UnityEngine;

namespace Extension
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour // Singleton Improve
    {
        protected static T instace = null;
        protected static bool isApplicationQuit;
        public static T Instance
        {
            get
            {

                if (isApplicationQuit)
                {
                    return null;
                }

                if (instace == null)
                {
                    if (FindObjectOfType<T>() != null) // Nếu tìm thấy gameObject có Type T sẽ gán instance = gameObject được tìm thấy
                    {
                        instace = FindObjectOfType<T>();
                    }
                    else
                    {
                        new GameObject().AddComponent<T>().name = "Singleton_"+typeof(T).ToString(); // nếu không tìm thấy thì khởi tạo gameObject và tự động thêm class T vào gameObject
                    }
                }
                return instace;
            }
        }

        protected virtual void Awake()
        {
            if(instace != null && instace != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instace = this.GetComponent<T>();
            }
        }

        private void OnApplicationQuit()
        {
            isApplicationQuit = true;
        }
    }
}
