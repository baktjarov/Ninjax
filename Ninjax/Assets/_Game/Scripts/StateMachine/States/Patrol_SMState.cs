using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachine
{
    public class Patrol_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private Transform _player;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private List<PatrolPoint> _patrolPoints = new List<PatrolPoint>();

        [Header("Settings")]
        [SerializeField] private float _seePlayerDistance = 7;
        [SerializeField] private float _patrolStoppingDistance = 2;

        [Header("States")]
        [SerializeField] private FindPlayer_SMState _findPlayerState;

        private int _currentPatrolPointIndex = 0;
        private bool _isIncreasingPatrolPointIndex = false;

        public override void Tick()
        {
            base.Tick();

            if (Vector3.Distance(transform.position, _player.position) > _seePlayerDistance)
            {
                GoToPatrolPoint(_currentPatrolPointIndex);
            }
            else
            {
                _nextState = _findPlayerState;
            }
        }

        private void GoToPatrolPoint(int patrolPointIndex)
        {
            _agent.SetDestination(_patrolPoints[patrolPointIndex].transform.position);
            _agent.stoppingDistance = _patrolStoppingDistance;

            if (_agent.remainingDistance <= _patrolStoppingDistance)
            {
                StartCoroutine(IncreasePatrolPointIndex());
            }
        }

        private IEnumerator IncreasePatrolPointIndex()
        {
            if (_isIncreasingPatrolPointIndex == true) { yield break; }
            _isIncreasingPatrolPointIndex = true;

            yield return new WaitForSecondsRealtime(_patrolPoints[_currentPatrolPointIndex].patrolTime);

            if (_agent.remainingDistance <= _patrolStoppingDistance)
            {
                _currentPatrolPointIndex++;
                if (_currentPatrolPointIndex >= _patrolPoints.Count)
                {
                    _currentPatrolPointIndex = 0;
                }
            }
            
            _isIncreasingPatrolPointIndex = false;
        }
    }
}