using UnityEngine;

namespace Scene
{
    public class SettingsCamera : MonoBehaviour
    {
        [SerializeField] private Texture2D _cursorTexture;
    
        void Start()
        {
            Cursor.SetCursor(_cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }
}
