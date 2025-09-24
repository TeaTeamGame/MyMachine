using Project.Core.Entities;

namespace Project.Core.Interfaces.UseCases
{
    public interface IMovementUseCase
    {
        void ProcessMovement(MovementInput input, MovementState state, MovementConfig config, float deltaTime);
        void ProcessJump(MovementInput input, MovementState state, MovementConfig config);
        void ProcessCrouch(MovementInput input, MovementState state, MovementConfig config);
        void ProcessSprint(MovementInput input, MovementState state, MovementConfig config);
        void ApplyGravity(MovementState state, MovementConfig config, float deltaTime);
    }
}