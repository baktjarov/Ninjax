using Characters;
using Interfaces;
using TagComponents;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class PlantFindPlayer_SMState : StateBase
    {
        [Header("Settings")]
        [SerializeField] private Plant _plant;

        [Header("States")]
        [SerializeField] private StateBase _shootState;
        [SerializeField] private StateBase _patrolState;

        [Inject] ISignalization<MainPlayer_Tag> _signalization;

        public override void Tick()
        {
            base.Tick();

            if (_plant.toAttack.Count > 0)
            {
                _nextState = _shootState;
            }
            else if (_signalization.noticedObjects.Count > 0)
            {
                var mainPlayer = _signalization.noticedObjects[0];

                if (mainPlayer != null)
                {
                    Vector3 lookAtPosition = mainPlayer.transform.position;
                    lookAtPosition.y = _plant.transform.position.y;

                    _plant.transform.LookAt(lookAtPosition);
                }
            }
            else
            {
                _nextState = _patrolState;
            }
        }
    }
}