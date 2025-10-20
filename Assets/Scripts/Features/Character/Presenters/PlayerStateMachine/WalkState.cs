using Features.Character.Models;
using UnityEngine;

namespace Features.Character.Presenters.PlayerStateMachine
{
    public class WalkState : PlayerMoveStateBase
    {
        public WalkState(MovementModel movementModel) : base(movementModel)
        {
        }

        public override void OnEnter()
        {
            MovementModel.TargetSpeed = MovementModel.Config.WalkSpeed;
        }

        public override void OnUpdate(float deltaTime)
        {
            MovementModel.UpdateSpeed();
            Debug.Log("WalkState");
        }
    }
}