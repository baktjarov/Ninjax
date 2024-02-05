using UnityEngine;

namespace StateMachine
{
    public class StateMachineBase : MonoBehaviour
    {
        [SerializeField] private StateBase _startState;
        private StateBase _currentState;

        private void Start()
        {
            ChangeState(_startState);
        }

        private void Update()
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