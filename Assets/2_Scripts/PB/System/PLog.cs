using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using Debug = UnityEngine.Debug;

namespace PB.SYSTEM
{
    public static class PLog
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(string message, [CallerMemberName] string callFunction = "", [CallerFilePath] string callClass = "")
        {
            var className = $"<color=white>{Path.GetFileNameWithoutExtension(callClass)}</color>";
            var logLevel = "<b><color=white>[ LOG ]</color></b>";
            var function = $"<color=yellow>{callFunction}()</color>";
            var msg = $"<color=white>{message}</color>";
            Debug.Log($"{logLevel} {className} :: {function} → {msg}");
        }

        [Conditional("UNITY_EDITOR")]
        public static void Warning(string message, [CallerMemberName] string callFunction = "", [CallerFilePath] string callClass = "")
        {
            var className = $"<color=white>{Path.GetFileNameWithoutExtension(callClass)}</color>";
            var logLevel = "<b><color=orange>[ WARNING ]</color></b>";
            var function = $"<color=yellow>{callFunction}()</color>";
            var msg = $"<color=white>{message}</color>";
            Debug.LogWarning($"{logLevel} {className} :: {function} → {msg}");
        }

        [Conditional("UNITY_EDITOR")]
        public static void Error(string message, [CallerMemberName] string callFunction = "", [CallerFilePath] string callClass = "")
        {
            var className = $"<color=white>{Path.GetFileNameWithoutExtension(callClass)}</color>";
            var logLevel = "<b><color=red>[ ERROR ]</color></b>";
            var function = $"<color=yellow>{callFunction}()</color>";
            var msg = $"<color=white>{message}</color>";
            Debug.LogError($"{logLevel} {className} :: {function} → {msg}");
        }

        [Conditional("UNITY_EDITOR")]
        public static void Test(string message, [CallerMemberName] string callFunction = "", [CallerFilePath] string callClass = "")
        {
            var className = $"<color=white>{Path.GetFileNameWithoutExtension(callClass)}</color>";
            var logLevel = "<b><color=green>[ TEST ]</color></b>";
            var function = $"<color=yellow>{callFunction}()</color>";
            var msg = $"<color=white>{message}</color>";
            Debug.Log($"{logLevel} {className} :: {function} → {msg}");
        }

        [Conditional("UNITY_EDITOR")]
        public static void ToDo(string message, [CallerMemberName] string callFunction = "", [CallerFilePath] string callClass = "")
        {
            var className = $"<color=white>{Path.GetFileNameWithoutExtension(callClass)}</color>";
            var logLevel = "<b><color=blue>[ TO DO ]</color></b>";
            var function = $"<color=yellow>{callFunction}()</color>";
            var msg = $"<color=white>{message}</color>";
            Debug.Log($"{logLevel} {className} :: {function} → {msg}");
        }

        [Conditional("UNITY_EDITOR")]
        public static void SystemLog(string message, [CallerMemberName] string callFunction = "", [CallerFilePath] string callClass = "")
        {
            var className = $"<color=white>{Path.GetFileNameWithoutExtension(callClass)}</color>";
            var logLevel = "<b><color=grey>[ SYSTEM ]</color></b>";
            var function = $"<color=yellow>{callFunction}()</color>";
            var msg = $"<color=white>{message}</color>";
            Debug.Log($"{logLevel} {className} :: {function} → {msg}");
        }
    }
}
