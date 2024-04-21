using DG.Tweening;
using Gameplay;
using Scriptables;
using Sensors;
using System.Collections.Generic;
using TagComponents;
using UnityEditor.Overlays;
using UnityEngine;

namespace Characters.MainPlayer
{
    public class PlayerShoot : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private AnimationEvents _animationEvents;
        [SerializeField] private BulletPooling _bulletPooling;
        [SerializeField] private Transform _shootPosition;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private VisionBase _visionSensor;

        [Header("Settings")]
        [SerializeField] private string _shootAnimationKey = "OnShoot";
        [SerializeField] private float _bulletSpeed = 25;
        [SerializeField] private float _damage = 25;

        [Header("Debug")]
        [SerializeField] private List<TagComponentBase> _currentVisibleEnemies = new();

        private bool _canShoot => _playerMovement.isMoving.value == false && _currentVisibleEnemies.Count > 0;

        private void OnEnable()
        {
            _animationEvents.onAnimationEvent += OnAnimationEvent;
            _playerMovement.isMoving.onValueChanged += OnMovementChanged;
            _visionSensor.onEnter += OnSensorEnter;
            _visionSensor.onExit += OnSensorExit;
        }

        private void OnDisable()
        {
            _animationEvents.onAnimationEvent -= OnAnimationEvent;
            _playerMovement.isMoving.onValueChanged -= OnMovementChanged;
            _visionSensor.onEnter -= OnSensorEnter;
            _visionSensor.onExit -= OnSensorExit;
        }

        private void Update()
        {
            if (_canShoot) { LookAtPlayer(); }
        }

        private void Shoot()
        {
            var bullet = _bulletPooling.Get(_shootPosition.position, _shootPosition.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _bulletSpeed;
            bullet.Inititlize(_bulletPooling);

            bullet.SetDamage(_damage);
        }

        private void OnAnimationEvent(string key)
        {
            if (_shootAnimationKey == key)
            {
                Shoot();
            }
        }

        private void OnMovementChanged(bool isMoving)
        {
            _animationEvents.animator.SetBool("IsAttacking", _canShoot);

            if (_canShoot == false)
            {
                _animationEvents.animator.Play("MovemenBlendTree");
            }
            else
            {
                _animationEvents.animator.Play("PreAttack");
            }
        }

        private void OnSensorEnter(TagComponentBase tag)
        {
            if (tag is IShootableEnemy_Tag && _currentVisibleEnemies.Contains(tag) == false) { _currentVisibleEnemies.Add(tag); }

            OnMovementChanged(_playerMovement.isMoving.value);
        }

        private void OnSensorExit(TagComponentBase tag)
        {
            if (tag is IShootableEnemy_Tag) { _currentVisibleEnemies.Remove(tag); }

            OnMovementChanged(_playerMovement.isMoving.value);
        }

        private void LookAtPlayer(float duration = 0.25f)
        {
            if(_currentVisibleEnemies.Count <= 0) { return; }   

            TagComponentBase mainPlayer = _currentVisibleEnemies[0];
            if(mainPlayer == null) { return; }

            Vector3 lookPosition = mainPlayer.transform.position;
            lookPosition.y = _animationEvents.transform.position.y;

            _animationEvents.transform.DOLookAt(lookPosition, duration);
        }
    }
}