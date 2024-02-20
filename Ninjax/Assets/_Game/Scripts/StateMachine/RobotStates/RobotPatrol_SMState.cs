using Characters;
using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachine
{
    public class RobotPatrol_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private Robot _robot;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private List<PatrolPoint> _patrolPoints = new List<PatrolPoint>();

        [Header("Settings")]
        [SerializeField] private float _seePlayerDistance = 7;
        [SerializeField] private float _patrolStoppingDistance = 2;

        [Header("States")]
        [SerializeField] private RobotFindPlayer_SMState _findPlayerState;

        [Header("Debug")]
        [SerializeField] private int _currentPatrolPointIndex = 0;
        [SerializeField] private bool _isIncreasingPatrolPointIndex = false;

        public override void Tick()
        {
            base.Tick();

            bool canSeePlayer = _robot.toAttack.Count > 0 &&
                Vector3.Distance(transform.position, _robot.toAttack[0].transform.position) > _seePlayerDistance;

            if (canSeePlayer == false)
            {
                GoToPatrolPoint(_currentPatrolPointIndex);
            }
            else
            {
                _agent.ResetPath();
                StopAllCoroutines();
                _nextState = _findPlayerState;
            }
        }

        private void GoToPatrolPoint(int patrolPointIndex)
        {
            Vector3 targetPatrolPosition = _patrolPoints[patrolPointIndex].transform.position;

            _agent.SetDestination(targetPatrolPosition);
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