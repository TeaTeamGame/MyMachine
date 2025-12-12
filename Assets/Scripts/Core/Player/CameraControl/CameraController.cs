using Core.CameraSystem;
using Core.Player.CursorControl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player.CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        
        private InputAction _moveAction;
        private InputAction _upDownAction;
        private InputAction _rotateAction;
        private InputAction _zoomAction;

        private CameraControlValue _cameraControlValue;
        private CameraControlModel _cameraControlModel;
        private bool _isSetup;
        private bool _isEnabled;
        
        private void Start()
        {
            _cameraControlValue = CameraControlValue.GetInstance();
            _cameraControlModel = new CameraControlModel(_cameraControlValue, CameraManager.Instance.MainCamera);
            
            _moveAction = playerInput.actions["Move"];
            _upDownAction = playerInput.actions["UpDown"];
            _rotateAction = playerInput.actions["Look"];
            _zoomAction = playerInput.actions["Zoom"];

            _isSetup = true;
        }

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }

        private void Update()
        {
            if (!_isSetup) return;
            
            var horizontalDir = _moveAction.ReadValue<Vector2>();
            var verticalValue = _upDownAction.ReadValue<float>();

            var rotateValue = Vector2.zero;
            if (!CursorController.Instance.IsShowingCursor) rotateValue = _rotateAction.ReadValue<Vector2>();
            var zoomValue = _zoomAction.ReadValue<float>();

            var movementData = new MovementData
            {
                HorizontalMovement = horizontalDir,
                VerticalMovement = verticalValue,
                RotateMovement =  rotateValue,
                ZoomOffset = zoomValue
            };
            
            _cameraControlModel.Move(movementData, Time.deltaTime);
        }
    }
}
