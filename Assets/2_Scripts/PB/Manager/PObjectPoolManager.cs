using PB.SYSTEM;
using PB.UTILS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PB.MANAGER
{
    public class PObjectPoolManager : PMonoSingleton<PObjectPoolManager>
    {
        [Serializable]
        public class Pool
        {
            public string Key;
            public int StartPoolSize;
            public bool ClearContainerOnStart = true;
            public GameObject PoolObject;
            public Transform Container;
        }

        [SerializeField] List<Pool> pools;
        Dictionary<string, Queue<GameObject>> poolsDict;

        protected override void Awake()
        {
            base.Awake();

            poolsDict = new Dictionary<string, Queue<GameObject>>();

            foreach (var pool in pools)
            {
                var objectPool = new Queue<GameObject>();

                if (pool.ClearContainerOnStart)
                {
                    pool.Container.DestroyAllChildren();
                }

                for (int i = 0; i < pool.StartPoolSize; i++)
                {
                    var obj = Instantiate(pool.PoolObject, pool.Container);
                    objectPool.Enqueue(obj);
                    obj.SetActive(false);
                }

                poolsDict.Add(pool.Key, objectPool);
            }
        }

        public GameObject Get(string key)
        {
            if (HasPool(key) == false) return null;

            var objectPool = poolsDict[key];
            if (objectPool == null || objectPool.Count == 0)
            {
                CreateNewObj(key);
            }

            var obj = objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        public T Get<T>(string key) where T : Component
        {
            var obj = Get(key);
            return obj.GetComponent<T>();
        }

        public GameObject Get(string key, Vector2 position)
        {
            var obj = Get(key);
            obj.transform.position = position;
            return obj;
        }

        public GameObject Get(string key, Vector3 position)
        {
            var obj = Get(key);
            obj.transform.position = position;
            return obj;
        }

        public GameObject Get(string key, Vector3 position, Quaternion rotation)
        {
            var obj = Get(key);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

        public T Get<T>(string key, Vector2 position) where T : Component
        {
            var obj = Get(key, position);
            return obj.GetComponent<T>();
        }

        public T Get<T>(string key, Vector3 position) where T : Component
        {
            var obj = Get(key, position);
            return obj.GetComponent<T>();
        }

        public T Get<T>(string key, Vector3 position, Quaternion rotation) where T : Component
        {
            var obj = Get(key, position, rotation);
            return obj.GetComponent<T>();
        }

        public void Release(string key, GameObject obj)
        {
            if (HasPool(key) == false) return;

            obj.SetActive(false);
            poolsDict[key].Enqueue(obj);
        }
        
        public void ReleaseAll(string key)
        {
            if (HasPool(key) == false) return;

            var pool = pools.Find(x => x.Key == key);
            var container = pool.Container;

            for (int i = 0; i < container.childCount; i++)
            {
                var child = container.GetChild(i).gameObject;
                var alreadyInPool = poolsDict[key].Contains(child);
                if (alreadyInPool) continue;
                Release(key, child);
            }
        }

        private void CreateNewObj(string key)
        {
            var pool = pools.Find(x => x.Key == key);
            var prefab = pool.PoolObject;
            var container = pool.Container;
            var obj = Instantiate(prefab, container);

            poolsDict[key].Enqueue(obj);
            obj.SetActive(false);
        }

        private bool HasPool(string key)
        {
            if (poolsDict.ContainsKey(key))
            {
                return true;
            }
            else
            {
                PLog.Error($"PoolDictionary doesn't contain key: {key}");
                return false;
            }
        }
    }
}
