using PB.MANAGER;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PB.BUTTON
{
    [RequireComponent(typeof(Image))]
    public class PButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsPressed { get; private set; }

        public bool Interactable = true;
        [SerializeField] protected bool useColorChangeAnimationOnClick = true;
        [SerializeField] string buttonSfxKey = null;


        public UnityEvent onClick;
        private Image image;
        private Image Image => image ??= GetComponent<Image>();
        private Color defaultColor;
        private Color pressedColor;

        protected virtual void Awake()
        {
            defaultColor = Image.color;
            pressedColor = defaultColor * 0.8f;
        }

        public void SetColor(Color color)
        {
            Image.color = color;
        }

        public void SetInteractable(bool interactable)
        {
            Interactable = interactable;
            Image.color = interactable ? defaultColor : defaultColor * 0.5f;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (Interactable == false) return;

            onClick?.Invoke();
            TryPlaySfx();
        }

        protected void TryPlaySfx()
        {
            if (buttonSfxKey != null && buttonSfxKey != string.Empty)
            {
                PSoundManager.Instance.PlaySfx(buttonSfxKey);
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (Interactable == false) return;

            IsPressed = true;
            if (useColorChangeAnimationOnClick) Image.color = pressedColor;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (Interactable == false) return;

            IsPressed = false;
            if (useColorChangeAnimationOnClick) Image.color = defaultColor;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (Interactable == false) return;

            IsPressed = false;
            if (useColorChangeAnimationOnClick) Image.color = defaultColor;
        }
    }
}
