using Project.Application.UseCases;
using Project.Core.Entities;
using Project.Core.Interfaces.Repositories;
using Project.Core.Interfaces.UseCases;
using Project.Infrastructure.Input;
using Project.Infrastructure.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project._03_Presentation
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
    public class CharacterMovementController : MonoBehaviour
    {
        [Header("Movement configuration")]
        [SerializeField] private MovementConfig _movementConfig;
        
        [Header("Debug")]
        [SerializeField] private bool showDebugInfo = true;
        
        private CharacterController _characterController;
        private PlayerInput _playerInput;

        private IInputRepository _inputRepository;
        private IMovementUseCase _movementUseCase;
        private IMovementRepository _movementRepository;
        
        private MovementState _movementState;

        private void Awake()
        {
            InitializeDependencies();
            InitializeState();
        }

        private void InitializeDependencies()
        {
            _characterController = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();

            _movementUseCase = new MovementUseCase();
            _inputRepository = new UnityInputRepository(_playerInput);
            _movementRepository = new UnityMovementRepository(_characterController, transform);
        }

        private void InitializeState()
        {
            _movementState = new MovementState();

            if (_movementConfig == null)
            {
                _movementConfig = new MovementConfig();
            }
        }
        
        private void Update()
        {
            ProcessMovement();
        }

        private void ProcessMovement()
        {
            var input = _inputRepository.GetMovementInput();
            
            _movementState.IsGrounded = _movementRepository.CheckGrounded(transform.position, _movementConfig);
            
            _movementUseCase.ProcessMovement(input, _movementState, _movementConfig, Time.deltaTime);
            _movementUseCase.ProcessJump(input, _movementState, _movementConfig);
            _movementUseCase.ProcessCrouch(input, _movementState, _movementConfig);
            _movementUseCase.ProcessSprint(input, _movementState, _movementConfig);
            _movementUseCase.ApplyGravity(_movementState, _movementConfig, Time.deltaTime);

            _movementRepository.ApplyMovement(_movementState.Velocity, Time.deltaTime);
            
        }
        
        private void OnDrawGizmosSelected()
        {
            if (showDebugInfo && _movementConfig != null)
            {
                // Draw ground check ray
                Gizmos.color = _movementState?.IsGrounded == true ? Color.green : Color.red;
                Vector3 rayStart = transform.position + Vector3.up * 0.1f;
                Gizmos.DrawRay(rayStart, Vector3.down * (_movementConfig.GroundCheckDistance + 0.1f));
            
                // Draw velocity vector
                Gizmos.color = Color.blue;
                if (_movementState != null)
                {
                    Gizmos.DrawRay(transform.position + Vector3.up, _movementState.Velocity);
                }
            }
        }
    }
}