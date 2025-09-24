using Project.Core.Entities;
using Project.Core.Interfaces.Repositories;
using UnityEngine;

namespace Project.Infrastructure.Movement
{
    public class UnityMovementRepository : IMovementRepository
    {
        private readonly CharacterController _characterController;
        private readonly Transform _characterTransform;
        
        public UnityMovementRepository(CharacterController characterController, Transform characterTransform)
        {
            _characterController = characterController;
            _characterTransform = characterTransform;
        }
        
        public bool CheckGrounded(Vector3 position, MovementConfig config)
        {
            return _characterController.isGrounded;
        }

        public void ApplyMovement(Vector3 movement, float deltaTime)
        {
            _characterController.Move(movement * deltaTime);
        }
    }
}