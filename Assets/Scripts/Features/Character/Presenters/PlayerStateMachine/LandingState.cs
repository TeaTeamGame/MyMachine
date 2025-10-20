using Features.Character.Models;
using UnityEngine;

namespace Features.Character.Presenters.PlayerStateMachine
{
    public class LandingState : PlayerMoveStateBase
    {
        public LandingState(MovementModel movementModel) : base(movementModel)
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            Debug.Log("LandingState");
        }
    }
}