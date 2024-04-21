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
    public class PlantPatrol_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private Plant _plant;
        [SerializeField] private float _speed;
        [SerializeField] private List<PatrolPoint> _patrolPoints = new List<PatrolPoint>();

        [Header("States")]
        [SerializeField] private PlantFindPlayer_SMState _plantFindPlayerState;

        [Header("Debug")]
        [SerializeField] private int _currentPatrolPointIndex = 0;
        [SerializeField] private bool _isIncreasingPatrolPointIndex = false;

        [Inject] ISignalization<MainPlayer_Tag> _signalization;

        public override void Tick()
        {
            base.Tick();
            MainPlayer_Tag mainPlayer = null;

            if (_plant.toAttack.Count > 0) { mainPlayer = _plant.toAttack[0]; }
            else if (_signalization.noticedObjects.Count > 0) { mainPlayer = _signalization.noticedObjects[0]; }
            bool canSeePlayer = mainPlayer != null;

            if (canSeePlayer == true)
            {
                StopAllCoroutines();
                _nextState = _plantFindPlayerState;
            }
            else
            {
                RotateToPatrolPoint(_currentPatrolPointIndex);
            }
        }

        private void RotateToPatrolPoint(int patrolPointIndex)
        {
            Vector3 targetPatrolPosition = _patrolPoints[patrolPointIndex].transform.position;

            Vector3 directionToPatrolPoint = targetPatrolPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPatrolPoint);
            _plant.transform.rotation = Quaternion.RotateTowards(_plant.transform.rotation, targetRotation, _speed * Time.deltaTime);

            if (Quaternion.Angle(_plant.transform.rotation, targetRotation) < 0.1f)
            {
                StartCoroutine(IncreasePatrolPointIndex());
            }
        }

        private IEnumerator IncreasePatrolPointIndex()
        {
            if (_isIncreasingPatrolPointIndex == true) { yield break; }
            _isIncreasingPatrolPointIndex = true;

            yield return new WaitForSecondsRealtime(_patrolPoints[_currentPatrolPointIndex].patrolTime);

            _currentPatrolPointIndex++;
            if (_currentPatrolPointIndex >= _patrolPoints.Count)
            {
                _currentPatrolPointIndex = 0;
            }

            _isIncreasingPatrolPointIndex = false;
        }
    }
}