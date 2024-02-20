using UnityEngine;

namespace StateMachine
{
    public class StateMachineBase : MonoBehaviour
    {
        [SerializeField] private StateBase _currentState;
        [SerializeField] private StateBase _startState;

        private void Start()
        {
            ChangeState(_startState);
        }

        protected virtual void Update()
        {
            _currentState?.Tick();

            StateBase nextState = _currentState.GetNextState();
            if (nextState != null) { ChangeState(nextState); }
        }

        private void ChangeState(StateBase newState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = newState;
            _currentState.Enter();
        }
    }
}