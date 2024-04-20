using Characters;
using Gameplay;
using Interfaces;
using System.Collections;
using System.Collections.Generic;
using TagComponents;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class RobotPatrol_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private Robot _robot;
        [SerializeField] private List<PatrolPoint> _patrolPoints = new List<PatrolPoint>();

        [Header("Settings")]
        [SerializeField] private float _patrolStoppingDistance = 2;

        [Header("States")]
        [SerializeField] private RobotFindPlayer_SMState _findPlayerState;

        [Header("Debug")]
        [SerializeField] private int _currentPatrolPointIndex = 0;
        [SerializeField] private bool _isIncreasingPatrolPointIndex = false;

        [Inject] ISignalization<MainPlayer_TagComponent> _signalization;

        public override void Tick()
        {
            base.Tick();

            MainPlayer_TagComponent mainPlayer = null;

            if (_robot.toAttack.Count > 0) { mainPlayer = _robot.toAttack[0]; }
            else if (_signalization.noticedObjects.Count > 0) { mainPlayer = _signalization.noticedObjects[0]; }

            bool canSeePlayer = mainPlayer != null;

            if (canSeePlayer == true)
            {
                _robot.agent.ResetPath();
                StopAllCoroutines();
                _nextState = _findPlayerState;
            }
            else
            {
                GoToPatrolPoint(_currentPatrolPointIndex);
            }
        }

        private void GoToPatrolPoint(int patrolPointIndex)
        {
            Vector3 targetPatrolPosition = _patrolPoints[patrolPointIndex].transform.position;

            _robot.agent.SetDestination(targetPatrolPosition);
            _robot.agent.stoppingDistance = _patrolStoppingDistance;

            if (_robot.agent.remainingDistance <= _patrolStoppingDistance)
            {
                StartCoroutine(IncreasePatrolPointIndex());
            }
        }

        private IEnumerator IncreasePatrolPointIndex()
        {
            if (_isIncreasingPatrolPointIndex == true) { yield break; }
            _isIncreasingPatrolPointIndex = true;

            yield return new WaitForSecondsRealtime(_patrolPoints[_currentPatrolPointIndex].patrolTime);

            if (_robot.agent.remainingDistance <= _patrolStoppingDistance)
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