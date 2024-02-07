using System.Collections;
using System.Collections.Generic;
using Characters.MainPlayer;
using Gameplay;
using UnityEngine;

namespace Characters
{
    public class PlayerShoot : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private AnimationEvents _animationEvents;
        [SerializeField] private Rigidbody _bulletPrefab;
        [SerializeField] private Transform _shootPosition;
        [SerializeField] private PlayerMovement _playerMovement;

        [Header("Settings")]
        [SerializeField] private string _shootAnimationKey = "OnShoot";
        [SerializeField] private float _bulletSpeed = 250;

        private void OnEnable()
        {
            _animationEvents.onAnimationEvent += OnAnimationEvent;
            _playerMovement.isMoving.onValueChanged += OnMovementChanged;
            OnMovementChanged(_playerMovement.isMoving.value);
        }

        private void OnDisable()
        {
            _animationEvents.onAnimationEvent -= OnAnimationEvent;
            _playerMovement.isMoving.onValueChanged -= OnMovementChanged;
        }

        private void OnMovementChanged(bool isMoving)
        {
            _animationEvents.animator.SetBool("IsAttacking", !isMoving);
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
            var bullet = Instantiate(_bulletPrefab, _shootPosition.position, _shootPosition.rotation);
            bullet.AddForce(bullet.transform.forward * _bulletSpeed);
        }
    }
}