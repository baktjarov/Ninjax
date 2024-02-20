using UnityEngine;
using UnityEngine.AI;
using Characters;

namespace StateMachine
{
    public class RobotFindPlayer_SMState : StateBase
    {
        [Header("Settings")]
        [SerializeField] private Robot _robot;

        [Header("States")]
        [SerializeField] private StateBase _sayHelloState;
        [SerializeField] private StateBase _patrolState;

        public override void Tick()
        {
            base.Tick();

            if (_robot.toAttack.Count > 0)
            {
                _nextState = _sayHelloState;
            }
            else
            {
                _nextState = _patrolState;
            }
        }
    }
}