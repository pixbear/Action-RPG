using UnityEngine;

namespace PB.SYSTEM
{
    [DefaultExecutionOrder(-1000)]
    public class PMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
#if UNITY_2023_2_OR_NEWER
                    instance = FindAnyObjectByType<T>();
#elif UNITY_2022_3_OR_NEWER
                instance = FindObjectOfType<T>();
#endif

                    if (instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                // Destroy(gameObject);
            }
        }
    }
}
