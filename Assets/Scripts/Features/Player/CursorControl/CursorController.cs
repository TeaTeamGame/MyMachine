using General;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player.CursorControl
{
    public class CursorController : Singleton<CursorController>
    {
        [SerializeField] private PlayerInput playerInput;
        
        private InputAction _showCursorAction;

        public bool IsShowingCursor => Cursor.visible;
        
        public void Start()
        {
            _showCursorAction = playerInput.actions["ShowCursor"];

            Cursor.visible = false;
            StartControl();
        }

        public void StartControl()
        {
            _showCursorAction.performed += ToggleDisplayState;

        }

        public void StopControl()
        {
            _showCursorAction.performed -= ToggleDisplayState;
        }

        private static void ToggleDisplayState(InputAction.CallbackContext obj)
        {
            Cursor.visible = !Cursor.visible;
        }

        public void OnDestroy()
        {
            StopControl();
        }
    }
}