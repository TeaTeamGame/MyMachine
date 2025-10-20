using Generals.StateMachine.Condition;
using Generals.StateMachine.State;

namespace Generals.StateMachine.Transition
{
    public class Transition : ITransition
    {
        public Transition(IState targetState, ICondition condition)
        {
            TargetState = targetState;
            Condition = condition;
        }

        public IState TargetState { get; }
        public ICondition Condition { get; }
    }
}