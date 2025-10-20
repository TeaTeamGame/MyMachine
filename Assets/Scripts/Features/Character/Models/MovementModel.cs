using Features.Character.Data;
using Features.Character.Presenters.PlayerStateMachine;
using Generals.StateMachine;
using Generals.StateMachine.Condition;
using Generals.StateMachine.Transition;
using UnityEngine;

namespace Features.Character.Models
{
    public class MovementModel
    {
        private float Gravity => Config.Gravity;
        private float Speed { get; set; }
        private Vector2 XZVelocity { get; set; }
        private float YVelocity { get; set; }
        public bool IsGrounded { get; set; }
        
        public float TargetSpeed { get; set; }
        
        public Vector3 Velocity => new Vector3(XZVelocity.x, YVelocity, XZVelocity.y);
        
        public readonly MovementConfigSo Config;
        
        private readonly StateMachine _moveStateMachine;

        private Vector2 _moveInput;

        private bool _isRunInputPress;
        
        public MovementModel(MovementConfigSo config)
        {
            Config = config;
            _moveStateMachine = new StateMachine();
            var idleState = new IdleState(this);
            var inAirState = new InAirState(this);
            var landingState = new LandingState(this);
            var walkState = new WalkState(this);
            var runState = new RunState(this);
            
            _moveStateMachine.AddAnyTransition(new Transition(inAirState, new FunctionCondition(() => !IsGrounded)));
            _moveStateMachine.AddAnyTransition(new Transition(idleState, new FunctionCondition(() => IsGrounded && _moveInput == Vector2.zero)));
            
            _moveStateMachine.AddTransition(inAirState, new Transition(landingState, new FunctionCondition(() => IsGrounded)));
            
            _moveStateMachine.AddTransition(landingState, new Transition(idleState, new FunctionCondition(() => _moveInput == Vector2.zero)));
            _moveStateMachine.AddTransition(landingState, new Transition(walkState, new FunctionCondition(() => _moveInput != Vector2.zero)));
            
            _moveStateMachine.AddTransition(idleState, new Transition(walkState, new FunctionCondition(() => _moveInput != Vector2.zero)));
            _moveStateMachine.AddTransition(walkState, new Transition(runState, new FunctionCondition(() => _moveInput != Vector2.zero && _isRunInputPress)));
            _moveStateMachine.AddTransition(runState, new Transition(walkState, new FunctionCondition(() => _moveInput != Vector2.zero && !_isRunInputPress)));
            
            
            _moveStateMachine.EnterInitialState(idleState);
        }
        
        public void Move(Vector2 dir)
        {
            _moveInput = dir;
        }

        public void ToggleCrouch()
        {
            
        }

        public void ToggleRunState(bool state)
        {
            _isRunInputPress = state;
        }

        // Update speed by grounded state update
        public void UpdateSpeed()
        {
            if (!IsGrounded) return;
            
            if (Speed < TargetSpeed)
            {
                Speed = Mathf.Min(Speed + Config.Acceleration * Time.deltaTime, TargetSpeed);
            }
            else if (Speed > TargetSpeed)
            {
                Speed = Mathf.Max(Speed - Config.Deceleration * Time.deltaTime, TargetSpeed);
            }

            var direction = _moveInput;
            if (_moveInput == Vector2.zero)
            {
                direction = XZVelocity;
            }

            XZVelocity = direction.normalized * Speed;
        }

        public void Jump()
        {
            if (!IsGrounded) return;
            YVelocity = Mathf.Sqrt(Config.JumpHigh * -2f * Gravity);
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
            _moveStateMachine.Update(deltaTime);
            return Velocity * deltaTime;
        }
    }
}