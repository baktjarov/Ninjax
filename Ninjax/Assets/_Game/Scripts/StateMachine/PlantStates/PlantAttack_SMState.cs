using Characters;
using System.Linq;
using TagComponents;
using Gameplay;
using Scriptables;
using TMPro;
using UnityEngine;

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
        [SerializeField] private float _bulletSpeed = 250;

        [Header("States")]
        [SerializeField] private PlantFindPlayer_SMState _findPlayerState;

        [Header("Debug")]
        [SerializeField] private MainPlayer_TagComponent _currentPlayer;

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

            MainPlayer_TagComponent mainPlayer = null;

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
                Debug.Log("Shoot");
                Shoot();
            }
        }

        private void Shoot()
        {
            _animationEvents.transform.LookAt(_currentPlayer.transform.position);
    
            var bullet = _bulletPooling.Get(_shootPosition.position, _shootPosition.rotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 bulletDirection = (_currentPlayer.transform.position - _shootPosition.position).normalized;
            bullet.transform.forward = bulletDirection;
            bulletRigidbody.velocity = bulletDirection * _bulletSpeed;

            bullet.Inititlize(_bulletPooling);
        }
    }
}