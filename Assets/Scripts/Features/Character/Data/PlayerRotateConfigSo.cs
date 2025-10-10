using UnityEngine;

namespace Features.Character.Data
{
    [CreateAssetMenu(fileName = "RotateConfig", menuName = "Character/Control/RotateConfig", order = 1)]
    public class PlayerRotateConfigSo : ScriptableObject
    {
        [field: SerializeField] public float MouseSensitivity { get; private set; } = 1f;
        [field: SerializeField] public float MinVerticalAngle { get; private set; } = -90f;
        [field: SerializeField] public float MaxVerticalAngle { get; private set; } = 90f;
    }
}