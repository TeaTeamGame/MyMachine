using Generals.StateMachine.Condition;
using Generals.StateMachine.State;

namespace Generals.StateMachine.Transition
{
    public interface ITransition
    {
        IState TargetState { get; }
        ICondition Condition { get; }
    }
}