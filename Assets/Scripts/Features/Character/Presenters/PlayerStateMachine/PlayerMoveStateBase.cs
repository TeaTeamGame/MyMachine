using Features.Character.Models;
using Generals.StateMachine.State;

namespace Features.Character.Presenters.PlayerStateMachine
{
    public abstract class PlayerMoveStateBase : IState
    {
        protected MovementModel MovementModel;
        
        public PlayerMoveStateBase(MovementModel movementModel)
        {
            MovementModel = movementModel;
        }
        
        public virtual void OnEnter() {}

        public virtual void OnExit() {}

        public virtual void OnUpdate(float deltaTime) {}
    }
}