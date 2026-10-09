using System;
using System.Collections.Generic;
using PB.SYSTEM;
using UnityEngine;

namespace PB.BUTTON
{
    public class PTabButton : MonoBehaviour
    {
        [SerializeField] Color selectedColor = Color.white;
        [SerializeField] Color unselectedColor = Color.gray;

        public event Action<int> OnSelected;
        public int SelectedIndex => selectedIndex;
        private int selectedIndex = -1;
        private List<PButton> buttons;
        private PButton currentSelectedButton = null;

        private void Awake()
        {
            buttons = new();

            for (int i = 0; i < transform.childCount; i++)
            {
                var index = i;
                var child = transform.GetChild(i);
                var button = child.GetComponent<PButton>();
                button.onClick.AddListener(() => Select(index));
                button.SetColor(unselectedColor);
                buttons.Add(button);
            }
        }

        private void Init(int index)
        {
            if (index < 0 || index >= buttons.Count)
            {
                PLog.Error($"Index {index} is out of range for PTabButton with {buttons.Count} buttons.");
                return;
            }

            if (index == selectedIndex) return;

            selectedIndex = index;
            buttons[selectedIndex].SetColor(selectedColor);
            if (currentSelectedButton != null)
                currentSelectedButton.SetColor(unselectedColor);
            currentSelectedButton = buttons[selectedIndex];
        }

        public void Select(int index)
        {
            if (index < 0 || index >= buttons.Count)
            {
                PLog.Error($"Index {index} is out of range for PTabButton with {buttons.Count} buttons.");
                return;
            }

            if (index == selectedIndex) return;

            PLog.Log($"TabButton selected index: {index}");
            Init(index);
            OnSelected?.Invoke(selectedIndex);
        }
    }
}
