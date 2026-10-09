using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PB.SYSTEM;

namespace PB.BUTTON
{
    public class PSwitch : MonoBehaviour
    {
        public bool IsOn { get; private set; }
        public Action<bool> OnSwitch { get; set; }

        [SerializeField] Image imgOn;
        [SerializeField] Image imgOff;

        [SerializeField] Color colorOn;
        [SerializeField] Color colorOff;

        private PEventTrigger _btnOn;
        private PEventTrigger _btnOff;


        private void Awake()
        {
            _btnOn = imgOn.GetComponent<PEventTrigger>();
            _btnOff = imgOff.GetComponent<PEventTrigger>();

            _btnOn.AddEvent(EventTriggerType.PointerClick, () => Toggle(true));
            _btnOff.AddEvent(EventTriggerType.PointerClick, () => Toggle(false));
        }

        public void Init(bool value)
        {
            IsOn = value;
            UpdateColor();
        }

        public void Toggle(bool isOn)
        {
            if (IsOn == isOn) return;
            Init(isOn);
            OnSwitch?.Invoke(IsOn);
            PLog.Log($"Switch toggled to: {IsOn}");
        }

        private void UpdateColor()
        {
            if (IsOn)
            {
                imgOn.color = colorOn;
                imgOff.color = colorOff;
            }
            else
            {
                imgOn.color = colorOff;
                imgOff.color = colorOn;
            }
        }
    }
}

