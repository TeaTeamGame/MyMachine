using Features.Character.Models;
using UnityEngine;

namespace Features.Character.Presenters.PlayerStateMachine
{
    public class RunState : PlayerMoveStateBase
    {
        public RunState(MovementModel movementModel) : base(movementModel)
        {
        }
        
        public override void OnEnter()
        {
            MovementModel.TargetSpeed = MovementModel.Config.RunSpeed;
        }

        public override void OnUpdate(float deltaTime)
        {
            MovementModel.UpdateSpeed();
            Debug.Log("RunState");
        }
    }
}