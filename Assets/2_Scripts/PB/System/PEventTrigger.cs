using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PB.SYSTEM
{
    [RequireComponent(typeof(EventTrigger))]
    public class PEventTrigger : MonoBehaviour
    {
        private EventTrigger _eventTrigger;

        private bool _isInit = false;

        private void Init()
        {
            if (_isInit) return;
            _eventTrigger ??= GetComponent<EventTrigger>();
            _isInit = true;
        }

        public void AddEvent(EventTriggerType triggerType, Action callback)
        {
            Init();
            EventTrigger.Entry entry = new();
            entry.eventID = triggerType;
            entry.callback.AddListener((data) => callback());
            _eventTrigger.triggers.Add(entry);
        }

        public void ClearEvent()
        {
            Init();
            _eventTrigger.triggers.Clear();
        }
    }
}
