using General;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player.CursorControl
{
    public class CursorController : Singleton<CursorController>
    {
        [SerializeField] private PlayerInput playerInput;
        
        private InputAction _showCursorAction;
        private InputAction _mousePositionAction;

        public bool IsShowingCursor => Cursor.visible;
        public Vector2 CursorPosition => _mousePositionAction.ReadValue<Vector2>();
        
        public void Start()
        {
            _showCursorAction = playerInput.actions["ShowCursor"];
            _mousePositionAction = playerInput.actions["MousePosition"];
            

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