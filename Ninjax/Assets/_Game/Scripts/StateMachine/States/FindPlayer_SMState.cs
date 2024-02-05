using UnityEngine;
using UnityEngine.AI;

namespace StateMachine
{
    public class FindPlayer_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _player;

        [Header("Settings")]
        [SerializeField] private float _targetDistance = 5;

        [Header("States")]
        [SerializeField] private StateBase _sayHelloState;

        public override void Tick()
        {
            base.Tick();

            if (Vector3.Distance(transform.position, _player.position) <= _targetDistance)
            {
                _nextState = _sayHelloState;
            }
            else
            {
                _agent.SetDestination(_player.position);
                _agent.stoppingDistance = _targetDistance / 2;
            }
        }
    }
}