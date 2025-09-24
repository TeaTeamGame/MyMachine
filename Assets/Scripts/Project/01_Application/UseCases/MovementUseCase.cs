using Project.Core.Entities;
using Project.Core.Interfaces.UseCases;
using UnityEngine;

namespace Project.Application.UseCases
{
    public class MovementUseCase : IMovementUseCase
    {
        public void ProcessMovement(MovementInput input, MovementState state, MovementConfig config, float deltaTime)
        {
            if (input.MovementDirection.magnitude > 0)
            {
                // Determine target speed based on state
                var targetSpeed = GetTargetSpeed(state, config);
                
                // Calculate movement direction relative to camera
                var movementDirection = new Vector3(input.MovementDirection.x, 0, input.MovementDirection.y);
                movementDirection = Camera.main.transform.TransformDirection(movementDirection);
                movementDirection.y = 0;
                movementDirection.Normalize();
                
                // Apply movement
                var currentVelocity = state.Velocity;
                currentVelocity.x = movementDirection.x * targetSpeed;
                currentVelocity.z = movementDirection.z * targetSpeed;
                state.Velocity = currentVelocity;
                state.CurrentSpeed = targetSpeed;
            }
            else
            {
                // Gradually slow down when no input
                var currentVelocity = state.Velocity;
                currentVelocity.x = Mathf.Lerp(state.Velocity.x, 0, deltaTime * 10f);
                currentVelocity.z = Mathf.Lerp(state.Velocity.z, 0, deltaTime * 10f);
                state.Velocity = currentVelocity;
                state.CurrentSpeed = 0;
            }
        }
        
        public void ProcessJump(MovementInput input, MovementState state, MovementConfig config)
        {
            if (input.JumpPressed && state.IsGrounded && !state.IsJumping)
            {
                var currentVelocity = state.Velocity;
                currentVelocity.y = config.JumpForce;
                state.Velocity = currentVelocity;
                
                state.IsJumping = true;
            }
        }

        public void ProcessCrouch(MovementInput input, MovementState state, MovementConfig config)
        {
            if (input.CrouchPressed)
            {
                state.IsCrouching = !state.IsCrouching;
            }
        }

        public void ProcessSprint(MovementInput input, MovementState state, MovementConfig config)
        {
            state.IsSprinting = input.SprintPressed && state.IsGrounded;
        }

        public void ApplyGravity(MovementState state, MovementConfig config, float deltaTime)
        {
            var currentVelocity = state.Velocity;
            if (!state.IsGrounded)
            {
                currentVelocity.y += config.Gravity * deltaTime;
            }
            else if (state.Velocity.y < 0)
            {
                currentVelocity.y = -0.5f; // Small downward force to keep grounded
                state.IsJumping = false;
            }
            
            state.Velocity = currentVelocity;
        }
        
        private float GetTargetSpeed(MovementState state, MovementConfig config)
        {
            if (state.IsCrouching)
                return config.CrouchSpeed;
           
            if (state.IsSprinting)
                return config.SprintSpeed;

            return config.RunSpeed;
        }
    }
}