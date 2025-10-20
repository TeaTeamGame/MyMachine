using System;
using System.Collections.Generic;
using System.Linq;
using Generals.StateMachine.State;
using Generals.StateMachine.Transition;

namespace Generals.StateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type, StateNode> _stateNodes = new Dictionary<Type, StateNode>();
        private readonly List<ITransition> _anyTransitions = new List<ITransition>();
        private StateNode _currentState;

        public void Update(float deltaTime)
        {
            _currentState?.State.OnUpdate(deltaTime);
            foreach (var transition in _anyTransitions.Where(transition => transition.Condition.Check()))
            {
                ChangeState(transition.TargetState);
                return;
            }

            if (_currentState == null) return;
            
            foreach (var transition in _currentState.GetAllTransitions())
            {
                if (!transition.Condition.Check()) continue;
                ChangeState(transition.TargetState);
                return;
            }
        }
        
        public void EnterInitialState(IState initialState)
        {
            ChangeState(initialState);
        }

        public void AddState(IState state)
        {
            FindOrCreateNode(state);
        }

        public void AddTransition(IState fromState, ITransition transition)
        {
            var node = FindOrCreateNode(fromState);
            FindOrCreateNode(transition.TargetState);
            node.AddTransition(transition);
        }

        public void AddAnyTransition(ITransition transition)
        {
            FindOrCreateNode(transition.TargetState);
            _anyTransitions.Add(transition);
        }

        /// <summary>
        /// Create a new StateNode if it does not exist, otherwise return the existing one.
        /// </summary>
        private StateNode FindOrCreateNode(IState state)
        {
            var stateType = state.GetType();
            if (_stateNodes.TryGetValue(stateType, out var node))
                return node;
  
            _stateNodes[stateType] = new StateNode(state);
            return _stateNodes[stateType];
        }
        
        private void ChangeState(IState newState)
        {
            if (_currentState != null && _currentState.State.GetType() == newState.GetType())
                return;
            
            _currentState?.State.OnExit();
            var newStateType = newState.GetType();
            _currentState = _stateNodes[newStateType];
            _currentState.State.OnEnter();
        }

        private class StateNode
        {
            public readonly IState State;
            private readonly List<ITransition> _transitions;

            public StateNode(IState state)
            {
                State = state;
                _transitions = new List<ITransition>();
            }
            
            public void AddTransition(ITransition transition)
            {
                _transitions.Add(transition);
            }
            
            public IEnumerable<ITransition> GetAllTransitions()
            {
                return _transitions;
            }
        }
    }
}