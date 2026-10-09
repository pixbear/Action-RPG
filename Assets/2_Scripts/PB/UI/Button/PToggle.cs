using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PB.BUTTON
{
    public class PToggle : MonoBehaviour, IPointerClickHandler
    {
        public bool IsOn { get; private set; }
        public Action<bool> onToggle { get; set; }

        [SerializeField] Image icon;

        [SerializeField] Sprite spriteOn;
        [SerializeField] Sprite spriteOff;
        
        public void Initialize(bool isOn)
        {
            IsOn = isOn;
            icon.sprite = isOn ? spriteOn : spriteOff;
        }

        public void Toggle(bool isOn)
        {
            Initialize(isOn);
            onToggle?.Invoke(IsOn);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Toggle(!IsOn);
        }
    }
}
