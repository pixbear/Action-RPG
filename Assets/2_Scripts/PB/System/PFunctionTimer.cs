using System;
using System.Collections;
using UnityEngine;

namespace PB.UTILS
{
    public class PFunctionTimer
    {
        private class MonoBehaviourHook : MonoBehaviour
        {
            public void Run(Action action, float delay, bool useUnscaledTime)
            {
                StartCoroutine(TimerRoutine(action, delay, useUnscaledTime));
            }

            private IEnumerator TimerRoutine(Action action, float delay, bool useUnscaledTime)
            {
                if (useUnscaledTime)
                {
                    yield return new WaitForSecondsRealtime(delay);
                }
                else
                {
                    yield return new WaitForSeconds(delay);
                }

                action?.Invoke();
                Destroy(gameObject);
            }
        }

        public static void Run(Action action, float delay, bool useUnscaledTime = false)
        {
            GameObject obj = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
            MonoBehaviourHook hook = obj.GetComponent<MonoBehaviourHook>();
            hook.Run(action, delay, useUnscaledTime);
        }
    }
}