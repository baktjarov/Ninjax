using Characters;
using DG.Tweening;
using Interfaces;
using TagComponents;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class RobotFindPlayer_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private Robot _robot;

        [Header("Settings")]
        [SerializeField] private float _stoppingDistance = 5;

        [Header("States")]
        [SerializeField] private RobotShoot_SMState _shootState;
        [SerializeField] private RobotPatrol_SMState _patrolState;

        [Inject] ISignalization<MainPlayer_Tag> _signalization;

        public override void Enter()
        {
            base.Enter();
        }

        public override void Tick()
        {
            base.Tick();

            if (_robot.toAttack.Count > 0)
            {
                _nextState = _shootState;
            }
            else if (_signalization.noticedObjects.Count > 0)
            {
                var mainPlayer = _signalization.noticedObjects[0];

                if (mainPlayer != null)
                {
                    _robot.agent.stoppingDistance = _stoppingDistance;

                    _robot.agent.SetDestination(mainPlayer.transform.position);
                    if (_robot.agent.velocity == Vector3.zero)
                    {
                        _robot.transform.DOLookAt(mainPlayer.transform.position, 2);
                    }
                }
            }
            else
            {
                _robot.agent.stoppingDistance = 0.75f;

                if(Vector3.Distance(_robot.transform.position, _patrolState.lastSeenPlayerAt) > 1)
                {
                    _robot.agent.SetDestination(_patrolState.lastSeenPlayerAt);
                }
                else
                {
                    _nextState = _patrolState;
                }
            }
        }
    }
}