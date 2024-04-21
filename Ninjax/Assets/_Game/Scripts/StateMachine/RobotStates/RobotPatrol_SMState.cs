using Characters;
using Gameplay;
using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TagComponents;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class RobotPatrol_SMState : StateBase
    {
        [field: SerializeField] public Vector3 lastSeenPlayerAt { get; private set; }

        [Header("Components")]
        [SerializeField] private Robot _robot;
        [SerializeField] private Transform _patrolPointsParent;

        [Header("Settings")]
        [SerializeField] private float _patrolStoppingDistance = 2;

        [Header("States")]
        [SerializeField] private RobotFindPlayer_SMState _findPlayerState;

        [Header("Debug")]
        [SerializeField] private int _currentPatrolPointIndex = 0;
        [SerializeField] private bool _isIncreasingPatrolPointIndex = false;
        [SerializeField] private List<PatrolPoint> _patrolPoints = new List<PatrolPoint>();

        [Inject] ISignalization<MainPlayer_Tag> _signalization;

        public override void Enter()
        {
            base.Enter();

            _patrolPoints = _patrolPointsParent.GetComponentsInChildren<PatrolPoint>(true).ToList();
        }

        public override void Exit()
        {
            base.Exit();

            _isIncreasingPatrolPointIndex = false;
        }

        public override void Tick()
        {
            base.Tick();

            MainPlayer_Tag mainPlayer = null;

            if (_robot.toAttack.Count > 0) { mainPlayer = _robot.toAttack[0]; }
            else if (_signalization.noticedObjects.Count > 0) { mainPlayer = _signalization.noticedObjects[0]; }

            bool canSeePlayer = mainPlayer != null;

            if (canSeePlayer == true)
            {
                lastSeenPlayerAt = mainPlayer.transform.position;

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