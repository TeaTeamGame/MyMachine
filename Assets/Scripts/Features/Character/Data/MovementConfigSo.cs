using UnityEngine;

namespace Features.Character.Data
{
    [CreateAssetMenu(fileName = "MovementConfig", menuName = "Character/Control/MovementConfig", order = 0)]
    public class MovementConfigSo : ScriptableObject
    {
        [field: SerializeField] public float Acceleration { get; private set; } = 10f;
        [field: SerializeField] public float Deceleration { get; private set; } = 15f;
        [field: SerializeField] public float WalkSpeed { get; private set; } = 3f;
        [field: SerializeField] public float RunSpeed { get; private set; } = 8f;
        [field: SerializeField] public float CrouchSpeed { get; private set; } = 1.5f;
        [field: SerializeField] public float JumpHigh { get; private set; } = 10f;
        [field: SerializeField] public float Gravity { get; private set; } = -9.81f;
    }
}
