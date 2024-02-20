using System.Collections.Generic;
using Gameplay;
using Sensors;
using StateMachine;
using TagComponents;
using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
    public class Plant : StateMachineBase
    {
        public IReadOnlyList<MainPlayer_TagComponent> toAttack => _toAttack;

        [SerializeField] protected AnimationEvents _animationEvents;

        [SerializeField] private VisionSensor_Simple _vissionSensor;
        [SerializeField] private List<MainPlayer_TagComponent> _toAttack = new List<MainPlayer_TagComponent>();

        private void OnEnable()
        {
            _vissionSensor.onEnter += OnNotice;
            _vissionSensor.onExit += OnUnnotice;
        }

        private void OnDisable()
        {
            _vissionSensor.onEnter -= OnNotice;
            _vissionSensor.onExit -= OnUnnotice;
        }

        private void OnNotice(TagComponentBase tag)
        {
            MainPlayer_TagComponent mainPlayer = tag as MainPlayer_TagComponent;

            if (mainPlayer != null && _toAttack.Contains(mainPlayer) == false)
            {
                _toAttack.Add(mainPlayer);
            }
        }

        private void OnUnnotice(TagComponentBase tag)
        {
            MainPlayer_TagComponent mainPlayer = tag as MainPlayer_TagComponent;

            _toAttack.Remove(mainPlayer);
        }

    }
}