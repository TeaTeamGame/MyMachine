using Features.Character.Data;
using UnityEngine;

namespace Features.Character.Models
{
    public class MovementModel
    {
        private float Gravity => _config.Gravity;
        private float Speed { get; set; }
        private Vector2 XZVelocity { get; set; }
        private float YVelocity { get; set; }
        public bool IsGrounded { get; set; }

        private enum MoveState { Idle, Walking, Sprinting, Crouching, InAir }
        private MoveState _currentState = MoveState.Idle;
        
        public Vector3 Velocity => new Vector3(XZVelocity.x, YVelocity, XZVelocity.y);
        
        private readonly MovementConfigSo _config;
        
        public MovementModel(MovementConfigSo config)
        {
            _config = config;
        }
        
        public void Move(Vector2 dir)
        {
            if (!IsGrounded || _currentState == MoveState.InAir) return;

            if (dir == Vector2.zero)
            {
                if (_currentState == MoveState.Crouching)
                    return;
                
            }
                
            var maxSpeed = _currentState switch
            {
                MoveState.Sprinting => _config.RunSpeed,
                MoveState.Crouching => _config.CrouchSpeed,
                MoveState.Idle => 0f,
                MoveState.Walking => _config.WalkSpeed,
                _ => _config.WalkSpeed
            };

            if (dir == Vector2.zero)
            {
                _currentState = MoveState.Idle;
            }

            if (Speed < maxSpeed)
            {
                Speed = Mathf.Min(Speed + _config.Acceleration * Time.deltaTime, maxSpeed);
            }
            else if (Speed > maxSpeed)
            {
                Speed = Mathf.Clamp(Speed - _config.Deceleration * Time.deltaTime, 0, maxSpeed);
            }
            
            XZVelocity = dir.normalized * Speed;
        }

        public void ToggleCrouch()
        {
            if (_currentState == MoveState.Crouching)
            {
                _currentState = Speed > 0 ? MoveState.Walking : MoveState.Idle;
            }
        }

        public void Sprint()
        {
            
        }

        public void Jump()
        {
            if (!IsGrounded) return;
            YVelocity = Mathf.Sqrt(_config.JumpHigh * -2f * Gravity);
        }
        
        public void ApplyGravity(float deltaTime)
        {
            if (IsGrounded && YVelocity < 0)
            {
                YVelocity = 0f;
                return;
            }
            
            YVelocity += Gravity * deltaTime;
        }

        public Vector3 CalculateMovement(float deltaTime)
        {
            return Velocity * deltaTime;
        }
    }
}