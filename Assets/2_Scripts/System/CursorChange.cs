using UnityEngine;

public class CursorChange : MonoBehaviour
{
    [SerializeField] Texture2D cursorImg;


    private void Start()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }
}

