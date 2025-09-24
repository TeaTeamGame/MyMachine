using Project.Core.Interfaces.Repositories;
using Project.Core.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Infrastructure.Input
{
    public class UnityInputRepository : IInputRepository
    {
        private readonly PlayerInput _playerInput;
        private readonly InputAction _movementInput;
        private readonly InputAction _jumpInput;
        private readonly InputAction _crouchInput;
        private readonly InputAction _sprintInput;
        
        public UnityInputRepository(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _movementInput = playerInput.actions["Move"];
            _jumpInput = playerInput.actions["Jump"];
            _crouchInput = playerInput.actions["Crouch"];
            _sprintInput = playerInput.actions["Sprint"];
        }
        
        public MovementInput GetMovementInput()
        {
            return new MovementInput
            {
                MovementDirection = _movementInput.ReadValue<Vector2>(),
                JumpPressed = _jumpInput.ReadValue<bool>(),
                CrouchPressed = _crouchInput.ReadValue<bool>(),
                SprintPressed = _sprintInput.ReadValue<bool>(),
            };
        }
    }
}