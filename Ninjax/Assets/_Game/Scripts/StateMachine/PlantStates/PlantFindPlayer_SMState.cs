using UnityEngine;
using UnityEngine.AI;
using Characters;

namespace StateMachine
{
    public class PlantFindPlayer_SMState : StateBase
    {
        [Header("Settings")]
        [SerializeField] private Plant _plant;

        [Header("States")]
        [SerializeField] private StateBase _sayHelloState;
        [SerializeField] private StateBase _patrolState;

        public override void Tick()
        {
            base.Tick();

            if (_plant.toAttack.Count > 0)
            {
                _nextState = _sayHelloState;
            }
            else
            {
                _nextState = _patrolState;
            }
        }
    }
}