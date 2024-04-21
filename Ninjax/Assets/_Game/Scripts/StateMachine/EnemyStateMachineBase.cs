using Interfaces;
using Sensors;
using System.Collections.Generic;
using TagComponents;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class EnemyStateMachineBase : StateMachineBase, IReactToSignalization<MainPlayer_Tag>
    {
        public IReadOnlyList<MainPlayer_Tag> toAttack => _toAttack;
        public MainPlayer_Tag toAttack_FromSignalization { get; private set; }

        [SerializeField] protected VisionBase _vissionSensor;
        [SerializeField] protected List<MainPlayer_Tag> _toAttack = new List<MainPlayer_Tag>();

        [Inject] protected ISignalization<MainPlayer_Tag> _signalization;

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
            if (tag is MainPlayer_Tag mainPlayert)
            {
                OnNotice(mainPlayert);
            }
        }

        protected virtual void OnATagExit(TagComponentBase tag)
        {
            if (tag is MainPlayer_Tag mainPlayer)
            {
                OnUnotice(mainPlayer);
            }
        }

        protected virtual void OnNotice(MainPlayer_Tag mainPlayer)
        {
            if (_toAttack.Contains(mainPlayer) == false)
            {
                _toAttack.Add(mainPlayer);
            }
        }

        protected virtual void OnUnotice(MainPlayer_Tag mainPlayer)
        {
            _toAttack.Remove(mainPlayer);
        }

        public virtual void OnSignalization(MainPlayer_Tag noticedObject)
        {
            if (toAttack_FromSignalization == null)
            {
                toAttack_FromSignalization = noticedObject;
            }
        }
    }
}