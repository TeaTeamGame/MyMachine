using System;
using System.Collections;
using Core.CameraSystem;
using Core.Part;
using Core.Player.CursorControl;
using General;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player.DetectObject
{
    public class DetectPartObject : Singleton<DetectPartObject>
    {
        [SerializeField] private LayerMask detectLayer;
        [SerializeField] private float detectLength;
        
        public Action<PartObject> DetectedObjectAction;
        
        private Camera MainCamera => CameraManager.Instance.MainCamera;
        private PartObject _selectedPartObject;
        private bool _isDetecting;
        
        public void StartDetect()
        {
            _isDetecting = true;
            _selectedPartObject = null;
            
            StartCoroutine(EDetectPart());
            return;

            IEnumerator EDetectPart()
            {
                yield return new WaitWhile(() =>
                {
                    var ray = CursorController.Instance.IsShowingCursor ? 
                        CameraManager.Instance.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()) : 
                        new Ray(CameraManager.Instance.CameraPosition, CameraManager.Instance.CameraForward);
                    
                    if (Physics.Raycast(
                            ray,
                            out var hit,
                            detectLength,
                            detectLayer))
                    {
                        if (hit.collider.TryGetComponent<PartObject>(out _selectedPartObject))
                        {
                            DetectedObjectAction?.Invoke(_selectedPartObject);
                        }
                    }
                    
                    return _isDetecting;
                });
            }
        }

        public PartObject FinishDetect()
        {
            _isDetecting = false;
            StopAllCoroutines();
            return _selectedPartObject;
        }
    }
}