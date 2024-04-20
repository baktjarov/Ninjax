using Interfaces;
using Sensors;
using System.Collections.Generic;
using TagComponents;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class EnemyStateMachineBase : StateMachineBase, IReactToSignalization<MainPlayer_TagComponent>
    {
        public IReadOnlyList<MainPlayer_TagComponent> toAttack => _toAttack;
        public MainPlayer_TagComponent toAttack_FromSignalization { get; private set; }

        [SerializeField] protected VisionBase _vissionSensor;
        [SerializeField] protected List<MainPlayer_TagComponent> _toAttack = new List<MainPlayer_TagComponent>();

        [Inject] protected ISignalization<MainPlayer_TagComponent> _signalization;

        protected virtual void Awake()
        {
            if (_vissionSensor == null) { _vissionSensor = GetComponentInChildren<VisionBase>(); }
        }

        protected virtual void OnEnable()
        {
            _vissionSensor.onEnter += OnATagEnter;
            _vissionSensor.onExit += OnATagExit;

            _signalization.onSignalizationTriggerred += OnSignalization;
        }

        protected virtual void OnDisable()
        {
            _vissionSensor.onEnter -= OnATagEnter;
            _vissionSensor.onExit -= OnATagExit;

            _signalization.onSignalizationTriggerred -= OnSignalization;
        }

        protected virtual void OnATagEnter(TagComponentBase tag)
        {
            if (tag is MainPlayer_TagComponent mainPlayert)
            {
                OnNotice(mainPlayert);
            }
        }

        protected virtual void OnATagExit(TagComponentBase tag)
        {
            if (tag is MainPlayer_TagComponent mainPlayer)
            {
                OnUnotice(mainPlayer);
            }
        }

        protected virtual void OnNotice(MainPlayer_TagComponent mainPlayer)
        {
            if (_toAttack.Contains(mainPlayer) == false)
            {
                _toAttack.Add(mainPlayer);
            }
        }

        protected virtual void OnUnotice(MainPlayer_TagComponent mainPlayer)
        {
            _toAttack.Remove(mainPlayer);
        }

        public virtual void OnSignalization(MainPlayer_TagComponent noticedObject)
        {
            if (toAttack_FromSignalization == null)
            {
                toAttack_FromSignalization = noticedObject;
            }
        }
    }
}