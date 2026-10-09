using UnityEngine;

namespace PB.UTILS
{
    public static class PComponentUtils
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component =>
            go.TryGetComponent(out T comp) ? comp : go.AddComponent<T>();

        public static T GetOrAddComponent<T>(this Transform tr) where T : Component =>
            tr.gameObject.TryGetComponent(out T comp) ? comp : tr.gameObject.AddComponent<T>();
    }
}
