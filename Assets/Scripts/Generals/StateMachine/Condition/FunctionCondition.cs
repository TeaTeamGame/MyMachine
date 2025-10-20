using System;

namespace Generals.StateMachine.Condition
{
    public class FunctionCondition : ICondition
    {
        private readonly Func<bool> _conditionFunction;
        
        public FunctionCondition(Func<bool> conditionFunction)
        {
            _conditionFunction = conditionFunction;
        }

        public bool Check()
        {
            return _conditionFunction();
        }
    }
}