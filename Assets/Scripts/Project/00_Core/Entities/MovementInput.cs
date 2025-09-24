using UnityEngine;

namespace Project.Core.Entities
{
    public class MovementInput
    {
        public Vector2 MovementDirection { get; set; }
        public bool JumpPressed { get; set; }
        public bool JumpHeld { get; set; }
        public bool CrouchPressed { get; set; }
        public bool SprintPressed { get; set; }
    }
}