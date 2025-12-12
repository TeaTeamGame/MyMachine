using Features.Player.CursorControl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player.CameraControl
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        
        private InputAction _moveAction;
        private InputAction _upDownAction;
        private InputAction _rotateAction;
        private InputAction _zoomAction;

        private CameraControlValue _cameraControlValue;
        private CameraControlModel _cameraControlModel;
        private bool _isSetup;

        private void Start()
        {
            var mainCamera = Camera.main;
            _cameraControlValue = CameraControlValue.GetInstance();
            _cameraControlModel = new CameraControlModel(_cameraControlValue, mainCamera);
            
            _moveAction = playerInput.actions["Move"];
            _upDownAction = playerInput.actions["UpDown"];
            _rotateAction = playerInput.actions["Look"];
            _zoomAction = playerInput.actions["Zoom"];

            _isSetup = true;
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
