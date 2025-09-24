using Project.Core.Entities;
using UnityEngine;

namespace Project.Core.Interfaces.Repositories
{
    public interface IMovementRepository
    {
        bool CheckGrounded(Vector3 position, MovementConfig config);
        void ApplyMovement(Vector3 movement, float deltaTime);
    }
}