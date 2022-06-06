using UnityEngine;

namespace Scene
{
    public class SettingsCamera : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorTexture;
    
        void Start()
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }
}
