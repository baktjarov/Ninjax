using System.Collections;
using System.Collections.Generic;
using Characters;
using Gameplay;
using UnityEngine;

namespace StateMachine
{
    public class VidecamPatrol_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private Videcam _videcam;
        [SerializeField] private float _speed;
        [SerializeField] private List<PatrolPoint> _patrolPoints = new List<PatrolPoint>();

        [Header("Debug")]
        [SerializeField] private int _currentPatrolPointIndex = 0;
        [SerializeField] private bool _isIncreasingPatrolPointIndex = false;

        public override void Tick()
        {
            base.Tick();
            bool canSeePlayer = _videcam.toAttack.Count > 0 &&
                Vector3.Distance(transform.position, _videcam.toAttack[0].transform.position) > 0;

            if (!canSeePlayer)
            {
                RotateToPatrolPoint(_currentPatrolPointIndex);
            }
            else
            {
                StopAllCoroutines();
            }
        }

        private void RotateToPatrolPoint(int patrolPointIndex)
        {
            Vector3 targetPatrolPosition = _patrolPoints[patrolPointIndex].transform.position;

            Vector3 directionToPatrolPoint = targetPatrolPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPatrolPoint);
            _videcam.transform.rotation = Quaternion.RotateTowards(_videcam.transform.rotation, targetRotation, _speed * Time.deltaTime);

            if (Quaternion.Angle(_videcam.transform.rotation, targetRotation) < 0.1f)
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