using Gameplay;
using Scriptables;
using Sensors;
using TagComponents;
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
        [SerializeField] private VisionSensor_Simple _visionSensor;

        [Header("Settings")]
        [SerializeField] private string _shootAnimationKey = "OnShoot";
        [SerializeField] private float _bulletSpeed = 250;

        [Header("Debug")]
        [SerializeField] private Robot_TagComponent _currentRobot;

        private void OnEnable()
        {
            _animationEvents.onAnimationEvent += OnAnimationEvent;
            _playerMovement.isMoving.onValueChanged += OnMovementChanged;
            _visionSensor.onEnter += OnSensorEnter;
            _visionSensor.onExit += OnSensorExit;

            OnMovementChanged(_playerMovement.isMoving.value);
        }

        private void OnDisable()
        {
            _animationEvents.onAnimationEvent -= OnAnimationEvent;
            _playerMovement.isMoving.onValueChanged -= OnMovementChanged;
            _visionSensor.onEnter -= OnSensorEnter;
            _visionSensor.onExit -= OnSensorExit;
        }

        private void Shoot()
        {
            _animationEvents.transform.LookAt(_currentRobot.transform.position);

            var bullet = _bulletPooling.Get(_shootPosition.position, _shootPosition.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _bulletSpeed;
            bullet.Inititlize(_bulletPooling);
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
            bool canShoot = isMoving == false && _currentRobot != null;

            _animationEvents.animator.SetBool("IsAttacking", canShoot);

            if (canShoot == false)
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
            if (tag is Robot_TagComponent) { _currentRobot = tag as Robot_TagComponent; }
            OnMovementChanged(_playerMovement.isMoving.value);
        }

        private void OnSensorExit(TagComponentBase tag)
        {
            if (tag is Robot_TagComponent) { _currentRobot = null; }
            OnMovementChanged(_playerMovement.isMoving.value);
        }
    }
}