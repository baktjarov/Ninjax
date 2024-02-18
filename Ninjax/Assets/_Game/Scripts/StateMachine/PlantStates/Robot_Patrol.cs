using Characters;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachine
{
    public class Robot_Patrol : StateBase
    {
        [Header("Components")]
        [SerializeField] private Plant _plant;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _speed;

        [Header("Settings")]
        [SerializeField] private float _seePlayerDistance = 5;

        [Header("States")]
        [SerializeField] private Robot_Attack _robotAtackState;

        public override void Tick()
        {
            base.Tick();
            bool canSeePlayer = _plant.toAttack.Count > 0 &&
                Vector3.Distance(transform.position, _plant.toAttack[0].transform.position) > _seePlayerDistance;

            if (canSeePlayer == false)
            {
                _plant.transform.eulerAngles += Vector3.up * _speed;
            }
            else
            {
                _nextState = _robotAtackState;
            }

        }

    }
}