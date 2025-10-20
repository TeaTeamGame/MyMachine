using Features.Character.Models;
using UnityEngine;

namespace Features.Character.Presenters.PlayerStateMachine
{
    /// <summary>
    /// In ground and not have movement input
    /// </summary>
    public class IdleState : PlayerMoveStateBase
    {
        public IdleState(MovementModel movementModel) : base(movementModel)
        {
            
        }

        public override void OnEnter()
        {
            MovementModel.TargetSpeed = 0f;
        }

        public override void OnUpdate(float deltaTime)
        {
            MovementModel.UpdateSpeed();
            Debug.Log("IdleState");
        }
    }
}