using Features.Character.Models;
using UnityEngine;

namespace Features.Character.Presenters.PlayerStateMachine
{
    public class InAirState : PlayerMoveStateBase
    {
        public InAirState(MovementModel movementModel) : base(movementModel)
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            Debug.Log("InAirState");
        }
    }
}