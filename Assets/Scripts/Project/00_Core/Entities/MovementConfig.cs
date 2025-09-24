using UnityEngine;

namespace Project.Core.Entities
{
    public class MovementConfig
    {
        public float WalkSpeed { get; set; } = 3f;
        public float RunSpeed { get; set; } = 5f;
        public float SprintSpeed { get; set; } = 7f;
        public float CrouchSpeed { get; set; } = 1.5f;
        public float JumpForce { get; set; } = 5f;
        public float Gravity { get; set; } = -9.81f;
        public float GroundCheckDistance { get; set; } = 0.1f;
        public LayerMask GroundLayerMask { get; set; } = 1;
    }
}