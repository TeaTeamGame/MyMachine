using Features.Character.Models;
using Features.Character.Views;

namespace Features.Character.Presenters
{
    public class PlayerPresenter
    {
        private readonly MovementModel _movementModel;
        private readonly PlayerRotationModel _rotationModel;
        private readonly PlayerView _view;
        
        public PlayerPresenter(MovementModel movementModel, PlayerRotationModel rotationModel, PlayerView view)
        {
            _movementModel = movementModel;
            _rotationModel = rotationModel;
            _view = view;

            _view.OnMoveInput += _movementModel.Move;
            _view.OnLookInput += _rotationModel.Look;
            _view.OnJumpInput += _movementModel.Jump;
            //_view.OnCrouchInput += HandleCrouch;
        }
        
        public void FixedUpdate(float fixedDeltaTime)
        {
            _movementModel.ApplyGravity(fixedDeltaTime);
        }
        
        public void Update(float deltaTime)
        {
            _movementModel.IsGrounded = _view.IsGrounded;
            var velocity = _movementModel.CalculateMovement(deltaTime);
            var lookValue = _rotationModel.CalculateRotation(deltaTime);
            
            _view.PerformMovement(velocity);
            _view.PerformLook(lookValue);
        }
    }
}