using UnityEngine;

namespace StateMachine
{
    public abstract class StateBase : MonoBehaviour
    {
        protected StateBase _nextState;

        public virtual StateBase GetNextState()
        {
            return _nextState;
        }

        public virtual void Enter()
        {
            _nextState = null;
        }

        public virtual void Exit()
        {
            _nextState = null;
        }

        public virtual void Tick() { }
    }
}