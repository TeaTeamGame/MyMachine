using System;
using Features.Character.Data;
using Features.Character.Models;
using Features.Character.Presenters;
using Features.Character.Views;
using Systems;
using UnityEngine;

namespace Features.Character
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MovementConfigSo movementConfig;
        [SerializeField] private PlayerRotateConfigSo rotateConfig;
        [SerializeField] private PlayerView playerView;

        private PlayerPresenter _presenter;
        
        private void Awake()
        {
            var movementModel = new MovementModel(movementConfig);
            var rotationModel = new PlayerRotationModel(rotateConfig);
            
            _presenter = new PlayerPresenter(movementModel, rotationModel, playerView);
            CursorSystem.SetCursorVisibility(false);
        }

        private void Update()
        {
            _presenter.Update(Time.deltaTime);
        }

        public void FixedUpdate()
        {
            _presenter.FixedUpdate(Time.fixedDeltaTime);
        }
    }
}