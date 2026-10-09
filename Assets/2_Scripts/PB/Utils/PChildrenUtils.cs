using UnityEngine;

namespace PB.UTILS
{
    public static class PChildrenUtils
    {
        #region Destroy
        public static void DestroyAllChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static void DestroyAllChildren(this GameObject gameObject)
        {
            foreach (Transform child in gameObject.transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static void DestroyAllChildren(this Transform[] transforms)
        {
            foreach (Transform transform in transforms)
            {
                transform.DestroyAllChildren();
            }
        }

        public static void DestroyAllChildren(this GameObject[] gameObjects)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.DestroyAllChildren();
            }
        }

#if UNITY_EDITOR
        public static void DestroyAllChildrenImmediate(this Transform transform)
        {
            while (transform.childCount > 0)
            {
                Object.DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        public static void DestroyAllChildrenImmediate(this GameObject gameObject)
        {
            while (gameObject.transform.childCount > 0)
            {
                Object.DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
            }
        }

        public static void DestroyAllChildrenImmediate(this Transform[] transforms)
        {
            foreach (Transform transform in transforms)
            {
                transform.DestroyAllChildrenImmediate();
            }
        }

        public static void DestroyAllChildrenImmediate(this GameObject[] gameObjects)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.DestroyAllChildrenImmediate();
            }
        }
#endif
        #endregion

        #region Active / Deactive
        public static void ActiveAllChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        public static void DeactiveAllChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        #endregion
    }
}
