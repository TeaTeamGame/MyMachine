using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Character.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private CinemachineCamera firstPersonCamera;
        
        [SerializeField] private float checkGroundDistance = 0.1f;

        public Action<Vector2> OnMoveInput;
        public Action<Vector2> OnLookInput;
        public Action OnJumpInput;
        public Action<bool> OnRunInput;
        public Action OnCrouchInput;

        private InputAction _moveAction;
        private InputAction _runPressAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;
        private InputAction _crouchAction;

        
        private void Awake()
        {
            Setup();
        }

        private void Setup()
        {
            var playerActionMap = inputActionAsset.FindActionMap("Player");
            _moveAction = playerActionMap.FindAction("Move");
            _runPressAction = playerActionMap.FindAction("Run");
            _lookAction = playerActionMap.FindAction("Look");
            _jumpAction = playerActionMap.FindAction("Jump");
            _crouchAction = playerActionMap.FindAction("Crouch");
            
            _lookAction.performed += ctx => OnLookInput?.Invoke(ctx.ReadValue<Vector2>());
            
            _runPressAction.performed += ctx => OnRunInput?.Invoke(true);
            _runPressAction.canceled += ctx => OnRunInput?.Invoke(false);
            
            _jumpAction.performed += ctx => OnJumpInput?.Invoke();
            _crouchAction.performed += ctx => OnCrouchInput?.Invoke();
        }

        public void Update()
        {
            var moveInputValue = _moveAction.ReadValue<Vector2>();
            var moveDirection = firstPersonCamera.transform.TransformDirection(new Vector3(moveInputValue.x, 0, moveInputValue.y));
            OnMoveInput?.Invoke(new Vector2(moveDirection.x, moveDirection.z));
        }

        public bool IsGrounded { get; private set; }

        public void FixedUpdate()
        {
            IsGrounded = Physics.Raycast(transform.position, Vector3.down, checkGroundDistance) || characterController.isGrounded;
        }

        public void PerformMovement(Vector3 velocity)
        {
            characterController.Move(velocity);
        }

        public void PerformLook(Vector2 look)
        {
            firstPersonCamera.transform.localRotation = Quaternion.Euler(look.y, look.x, 0);
        }
    }
}