namespace PB.SYSTEM
{
    public class PEnums { }

    public enum GameState
    {
        Loading,
        MainMenu,
        Playing,
        Paused,
        GameOver,
        Other,
    }

    public enum DirectionType
    {
        Left,
        Right,
        Up,
        Down,
    }

    public enum AnchoreType
    {
        None,
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        ExpendAll,
        ExpendTop,
        ExpendBottom,
        ExpendLeft,
        ExpendRight,
    }
}