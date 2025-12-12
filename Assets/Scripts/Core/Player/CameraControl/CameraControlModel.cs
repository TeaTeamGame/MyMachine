using Core.CameraSystem;
using UnityEngine;

namespace Core.Player.CameraControl
{
    public class CameraControlModel
    {
        private readonly CameraControlValue _value;
        private readonly Camera _camera;

        private float _zoomLevel;
        
        private readonly Transform _cameraTransform;
        
        public CameraControlModel(CameraControlValue cameraControlValue, Camera camera)
        {
            _value = cameraControlValue;
            _camera = camera;
            _cameraTransform = camera.transform;
        }

        public void Move(MovementData movementData, float deltaTime)
        {
           HandleZoom(movementData, deltaTime);
           HandleTransform(movementData, deltaTime);
           HandleRotate(movementData, deltaTime);
        }

        private void HandleZoom(MovementData movementData, float deltaTime)
        {
            _zoomLevel = Mathf.Clamp(_zoomLevel + movementData.ZoomOffset, _value.ZoomOffsetMin, _value.ZoomOffsetMax);
        }

        private void HandleTransform(MovementData movementData, float deltaTime)
        {
            var translate = Vector3.zero;
            if (movementData.HorizontalMovement != Vector2.zero)
            {
                var localDirection = new Vector3(movementData.HorizontalMovement.x, 0, movementData.HorizontalMovement.y);
                var worldDirection = _cameraTransform.TransformDirection(localDirection);
                translate += worldDirection.normalized * _value.MoveSpeed;
            }

            if (movementData.VerticalMovement != 0)
            {
                translate += Vector3.up * (movementData.VerticalMovement * _value.UpDownSpeed);
            }
            
            _cameraTransform.position += translate * deltaTime;
        }
        
        private void HandleRotate(MovementData movementData, float deltaTime)
        {
            if (movementData.RotateMovement == Vector2.zero) return;
            
            var rotateX = movementData.RotateMovement.y * _value.RotateSpeed * deltaTime;
            var rotateY = movementData.RotateMovement.x * _value.RotateSpeed * deltaTime;

            _cameraTransform.eulerAngles += new Vector3(-rotateX, rotateY, 0);
        }
        
    }
}