using PB.SYSTEM;
using UnityEngine;

namespace PB.UTILS
{
    public static class PRectUtils
    {
        #region Anchore
        public static void SetAnchore(this RectTransform rect, AnchoreType anchore)
        {
            switch (anchore)
            {
                case AnchoreType.TopLeft: rect.anchorMin = rect.anchorMax = new Vector2(0, 1); break;
                case AnchoreType.TopCenter: rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 1); break;
                case AnchoreType.TopRight: rect.anchorMin = rect.anchorMax = new Vector2(1, 1); break;
                case AnchoreType.MiddleLeft: rect.anchorMin = rect.anchorMax = new Vector2(0, 0.5f); break;
                case AnchoreType.MiddleCenter: rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f); break;
                case AnchoreType.MiddleRight: rect.anchorMin = rect.anchorMax = new Vector2(1, 0.5f); break;
                case AnchoreType.BottomLeft: rect.anchorMin = rect.anchorMax = new Vector2(0, 0); break;
                case AnchoreType.BottomCenter: rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0); break;
                case AnchoreType.BottomRight: rect.anchorMin = rect.anchorMax = new Vector2(1, 0); break;
                case AnchoreType.ExpendAll: rect.anchorMin = new Vector2(0, 0); rect.anchorMax = new Vector2(1, 1); break;
                case AnchoreType.ExpendTop: rect.anchorMin = new Vector2(0, 0.5f); rect.anchorMax = new Vector2(1, 1); break;
                case AnchoreType.ExpendBottom: rect.anchorMin = new Vector2(0, 0); rect.anchorMax = new Vector2(1, 0.5f); break;
                case AnchoreType.ExpendLeft: rect.anchorMin = new Vector2(0, 0); rect.anchorMax = new Vector2(0.5f, 1); break;
                case AnchoreType.ExpendRight: rect.anchorMin = new Vector2(0.5f, 0); rect.anchorMax = new Vector2(1, 1); break;
            }
        }

        public static AnchoreType GetAnchoreType(this RectTransform rect)
        {
            if (rect.anchorMin == new Vector2(0, 1) && rect.anchorMax == new Vector2(0, 1)) return AnchoreType.TopLeft;
            if (rect.anchorMin == new Vector2(0.5f, 1) && rect.anchorMax == new Vector2(0.5f, 1)) return AnchoreType.TopCenter;
            if (rect.anchorMin == new Vector2(1, 1) && rect.anchorMax == new Vector2(1, 1)) return AnchoreType.TopRight;
            if (rect.anchorMin == new Vector2(0, 0.5f) && rect.anchorMax == new Vector2(0, 0.5f)) return AnchoreType.MiddleLeft;
            if (rect.anchorMin == new Vector2(0.5f, 0.5f) && rect.anchorMax == new Vector2(0.5f, 0.5f)) return AnchoreType.MiddleCenter;
            if (rect.anchorMin == new Vector2(1, 0.5f) && rect.anchorMax == new Vector2(1, 0.5f)) return AnchoreType.MiddleRight;
            if (rect.anchorMin == new Vector2(0, 0) && rect.anchorMax == new Vector2(0, 0)) return AnchoreType.BottomLeft;
            if (rect.anchorMin == new Vector2(0.5f, 0) && rect.anchorMax == new Vector2(0.5f, 0)) return AnchoreType.BottomCenter;
            if (rect.anchorMin == new Vector2(1, 0) && rect.anchorMax == new Vector2(1, 0)) return AnchoreType.BottomRight;
            if (rect.anchorMin == new Vector2(0, 0) && rect.anchorMax == new Vector2(1, 1)) return AnchoreType.ExpendAll;
            if (rect.anchorMin == new Vector2(0, 0.5f) && rect.anchorMax == new Vector2(1, 1)) return AnchoreType.ExpendTop;
            if (rect.anchorMin == new Vector2(0, 0) && rect.anchorMax == new Vector2(1, 0.5f)) return AnchoreType.ExpendBottom;
            if (rect.anchorMin == new Vector2(0, 0) && rect.anchorMax == new Vector2(0.5f, 1)) return AnchoreType.ExpendLeft;
            if (rect.anchorMin == new Vector2(0.5f, 0) && rect.anchorMax == new Vector2(1, 1)) return AnchoreType.ExpendRight;
            return AnchoreType.None;
        }
        #endregion
    }
}
