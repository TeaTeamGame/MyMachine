using Features.Character.Data;
using UnityEngine;

namespace Features.Character.Models
{
    public class PlayerRotationModel
    {
        private readonly PlayerRotateConfigSo _configSo;

        private float _verticalRotation;
        private float _horizontalRotation;

        private Vector2 _mouseDelta;
        
        public PlayerRotationModel(PlayerRotateConfigSo configSo)
        {
            _configSo = configSo;
        }
        
        public void Look(Vector2 mouseDelta)
        {
            _mouseDelta = mouseDelta;
        }

        public Vector2 CalculateRotation(float deltaTime)
        {
            _verticalRotation -= _mouseDelta.y * _configSo.MouseSensitivity * deltaTime;
            _verticalRotation = Mathf.Clamp(_verticalRotation, _configSo.MinVerticalAngle, _configSo.MaxVerticalAngle);
            
            _horizontalRotation += _mouseDelta.x * _configSo.MouseSensitivity * deltaTime;
            _horizontalRotation %= 360f;
            _mouseDelta = Vector2.zero;

            return new Vector2(_horizontalRotation, _verticalRotation);
        }

        public Vector2 ResetRotation()
        {
            _verticalRotation = 0;
            _horizontalRotation = 0;
            return Vector2.zero;
        }
    }
}