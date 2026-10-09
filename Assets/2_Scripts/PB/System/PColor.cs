using UnityEngine;

namespace PB.SYSTEM
{
    public static class PColor
    {
        public static Color SetAlpha(this Color color, byte a) => new Color(color.r, color.g, color.b, a / 255f);
        public static Color SetAlpha(this Color color, float a) => new Color(color.r, color.g, color.b, a);
        public static Color White => new Color32(245, 245, 245, 255); // F5F5F5
        public static Color Grey => new Color32(130, 130, 140, 255); // 82828C
        public static Color Black => new Color32(20, 20, 20, 255); // 141414
        public static Color Red => new Color32(220, 70, 70, 255); // DC4646
        public static Color Green => new Color32(90, 180, 100, 255); // 5AB464
        public static Color DarkGreen => new Color32(50, 100, 50, 255); // 326432 
        public static Color Blue => new Color32(80, 130, 230, 255); // 5082E6  
        public static Color DarkBlue => new Color32(40, 60, 110, 255); // 283C6E  
        public static Color Yellow => new Color32(240, 220, 100, 255); // F0DC64 
        public static Color Orange => new Color32(255, 160, 90, 255); // FFA05A  
        public static Color Purple => new Color32(150, 100, 180, 255); // 9664B4 
    }
}
