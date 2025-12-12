using UnityEngine;

namespace Core.Player.CameraControl
{
    [CreateAssetMenu(fileName = "CameraControlValue", menuName = "Camera/CameraControlValue", order = 0)]
    public class CameraControlValue : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 10f;
        [field: SerializeField] public float UpDownSpeed { get; private set; } = 10f;
        [field: SerializeField] public float RotateSpeed { get; private set; } = 100f;
        [field: SerializeField] public float ZoomSpeed { get; private set; } = 10f;
        [field: SerializeField] public float ZoomOffsetMin { get; private set; } = -10f;
        [field: SerializeField] public float ZoomOffsetMax { get; private set; } = 10f;
        
        public static CameraControlValue GetInstance()
        {
            return Resources.Load<CameraControlValue>("CameraControlValue");
        }
    }
}