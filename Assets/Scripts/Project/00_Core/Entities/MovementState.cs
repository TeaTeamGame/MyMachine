using UnityEngine;

namespace Project.Core.Entities
{
    public class MovementState
    {
        public Vector3 Velocity { get; set; }
        public bool IsGrounded { get; set; }
        public bool IsJumping { get; set; }
        public bool IsCrouching { get; set; }
        public bool IsSprinting { get; set; }
        public float CurrentSpeed { get; set; }
    }
}