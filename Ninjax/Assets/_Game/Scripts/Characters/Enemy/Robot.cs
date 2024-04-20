using Gameplay;
using StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
    public class Robot : EnemyStateMachineBase
    {
        [field: SerializeField] public NavMeshAgent agent { get; private set; }

        [SerializeField] protected AnimationEvents _animationEvents;

        protected override void Update()
        {
            base.Update();

            _animationEvents.animator.SetFloat("Movement", agent.velocity.magnitude);
        }
    }
}