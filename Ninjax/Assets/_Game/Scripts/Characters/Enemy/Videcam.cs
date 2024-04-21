using Interfaces;
using StateMachine;
using System;
using TagComponents;
using UnityEngine;

namespace Characters
{
    public class Videcam : EnemyStateMachineBase, ISignalizer<MainPlayer_Tag>
    {
        public Action<MainPlayer_Tag> onSignalize { get; set; }

        [SerializeField] private Animator _animator;

        protected override void OnEnable()
        {
            base.OnEnable();

            _animator.SetBool("Sirene", false);
        }

        protected override void OnNotice(MainPlayer_Tag mainPlayer)
        {
            base.OnNotice(mainPlayer);

            Signalize(mainPlayer);
        }

        public override void OnSignalization(MainPlayer_Tag noticedObject)
        {

        }

        public  void Signalize(MainPlayer_Tag noticedObject)
        {
            onSignalize?.Invoke(noticedObject);

            _animator.SetBool("Sirene", true);
        }
    }
}