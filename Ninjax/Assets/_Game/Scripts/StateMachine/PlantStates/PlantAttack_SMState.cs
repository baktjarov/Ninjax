using Characters;
using System.Linq;
using TagComponents;
using Gameplay;
using Scriptables;
using UnityEngine;
using DG.Tweening;

namespace StateMachine
{
    public class PlantAttack_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private Plant _plant;
        [SerializeField] private AnimationEvents _animationEvents;
        [SerializeField] private BulletPooling _bulletPooling;
        [SerializeField] private Transform _shootPosition;

        [Header("Settings")]
        [SerializeField] private string _shootAnimationKey = "Shoot";
        [SerializeField] private float _bulletSpeed = 25;
        
        [SerializeField] private float _damage = 50;

        [Header("States")]
        [SerializeField] private PlantFindPlayer_SMState _findPlayerState;

        [Header("Debug")]
        [SerializeField] private MainPlayer_Tag _currentPlayer;

        private void OnEnable()
        {
            _animationEvents.onAnimationEvent += OnAnimationEvent;
        }

        private void OnDisable()
        {
            _animationEvents.onAnimationEvent += OnAnimationEvent;
        }

        public override void Tick()
        {
            base.Tick();

            MainPlayer_Tag mainPlayer = null;

            if (_plant.toAttack.Count > 0)
            {
                mainPlayer = _plant.toAttack.ElementAt(0);
                _currentPlayer = mainPlayer;
            }

            if (mainPlayer != null)
            {
                _animationEvents.animator.Play("Shoot");
            }
            else
            {
                _nextState = _findPlayerState;
            }
        }

        private void OnAnimationEvent(string key)
        {
            if (_shootAnimationKey == key)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            _animationEvents.transform.DOLookAt(_currentPlayer.transform.position, 0.25f);
            _shootPosition.LookAt(_currentPlayer.transform.position);

            var bullet = _bulletPooling.Get(_shootPosition.position, _shootPosition.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _bulletSpeed;
            bullet.Inititlize(_bulletPooling);

            bullet.SetDamage(_damage);
        }
    }
}