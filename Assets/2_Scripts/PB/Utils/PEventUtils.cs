using UnityEngine.Events;

namespace PB.UTILS
{
    public static class PEventUtils
    {
        public static void RemoveAndAddListener(this UnityEvent unityEvent, UnityAction unityAction)
        {
            unityEvent.RemoveAllListeners();
            unityEvent.AddListener(unityAction);
        }
    }
}
