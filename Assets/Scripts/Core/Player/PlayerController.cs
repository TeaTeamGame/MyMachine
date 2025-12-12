using Core.Part;
using Core.Player.CameraControl;
using Core.Player.DetectObject;
using General;
using UnityEngine;

namespace Core.Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        // Handle select part
        [SerializeField] private DetectPartObject detectPartObject;
        [SerializeField] private CameraController cameraController;

        private void Start()
        {
            cameraController.SetEnabled(true);
            detectPartObject.DetectedObjectAction += OnDetectedPartObject;
            detectPartObject.StartDetect();
        }

        private void OnDetectedPartObject(PartObject obj)
        {
            print(obj.name);
        }
    }
}