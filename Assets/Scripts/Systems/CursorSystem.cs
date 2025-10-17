using UnityEngine;

namespace Systems
{
    public static class CursorSystem
    {
        public static void SetCursorVisibility(bool isVisible)
        {
            Cursor.visible = isVisible;
            Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}