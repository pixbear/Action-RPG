using System.Collections.Generic;
using PB.UTILS;
using UnityEngine;

namespace PB.SYSTEM
{
    public class PObjectPooler : MonoBehaviour
    {
        [SerializeField] int startPoolSize = 0;
        [SerializeField] bool clearContainerOnStart = true;
        [SerializeField] GameObject poolObject;
        private Queue<GameObject> pools;

        private void Awake()
        {
            pools = new Queue<GameObject>();

            if (clearContainerOnStart)
            {
                transform.DestroyAllChildren();
            }

            if (startPoolSize > 0)
            {
                for (int i = 0; i < startPoolSize; i++)
                {
                    var obj = Instantiate(poolObject, transform);
                    pools.Enqueue(obj);
                    obj.SetActive(false);
                }
            }
        }

        public GameObject Get()
        {
            if (pools == null || pools.Count == 0)
            {
                CreateNewObj();
            }
            var obj = pools.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        public T Get<T>() where T : Component
        {
            var obj = Get();
            return obj.GetComponent<T>();
        }

        public GameObject Get(Vector2 position)
        {
            var obj = Get();
            obj.transform.position = position;
            return obj;
        }

        public GameObject Get(Vector3 position)
        {
            var obj = Get();
            obj.transform.position = position;
            return obj;
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            var obj = Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

        public T Get<T>(Vector2 position) where T : Component
        {
            var obj = Get(position);
            return obj.GetComponent<T>();
        }

        public T Get<T>(Vector3 position) where T : Component
        {
            var obj = Get(position);
            return obj.GetComponent<T>();
        }

        public T Get<T>(Vector3 position, Quaternion rotation) where T : Component
        {
            var obj = Get(position, rotation);
            return obj.GetComponent<T>();
        }

        public void Release(GameObject obj)
        {
            obj.SetActive(false);
            pools.Enqueue(obj);
        }

        public void ReleaseAll()
        {
            pools.Clear();

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                child.SetActive(false);
                pools.Enqueue(child);
            }
        }

        private void CreateNewObj()
        {
            var obj = Instantiate(poolObject, transform);
            pools.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}

