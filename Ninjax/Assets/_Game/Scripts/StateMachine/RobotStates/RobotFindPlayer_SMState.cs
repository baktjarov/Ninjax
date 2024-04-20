using Characters;
using Interfaces;
using TagComponents;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class RobotFindPlayer_SMState : StateBase
    {
        [Header("Settings")]
        [SerializeField] private Robot _robot;

        [Header("States")]
        [SerializeField] private StateBase _shootState;
        [SerializeField] private StateBase _patrolState;

        [Inject] ISignalization<MainPlayer_TagComponent> _signalization;

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
                    _robot.agent.SetDestination(mainPlayer.transform.position);
                }
                else
                {
                    _robot.agent.ResetPath();
                }
            }
            else
            {
                _nextState = _patrolState;
            }
        }
    }
}