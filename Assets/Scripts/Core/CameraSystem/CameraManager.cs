using General;
using UnityEngine;

namespace Core.CameraSystem
{
    public class CameraManager : Singleton<CameraManager>
    {
        public Camera MainCamera { get; private set; }
        
        public Vector3 CameraPosition => MainCamera.transform.position;
        public Vector3 CameraForward => MainCamera.transform.forward;
        

        public override void Awake()
        {
            base.Awake();
            MainCamera = Camera.main;
        }
    }
}