using Characters;
using DG.Tweening;
using Scriptables;
using System.Collections;
using System.Linq;
using TagComponents;
using UnityEngine;

namespace StateMachine
{
    public class RobotShoot_SMState : StateBase
    {
        [Header("Components")]
        [SerializeField] private Robot _robot;
        [SerializeField] private BulletPooling _bulletPooling;
        [SerializeField] private Transform _shootPosition;

        [Header("States")]
        [SerializeField] private RobotFindPlayer_SMState _findPlayerState;

        [Header("Settings")]
        [SerializeField] private float _shootRate = 25;
        [SerializeField] private float _bulletSpeed = 25;
        [SerializeField] private float _damage = 50;

        [Header("Debug")]
        [SerializeField] private bool _canShoot;

        MainPlayer_Tag _currentPlayer = null;

        public override void Enter()
        {
            base.Enter();

            StartCoroutine(ResetCanShoot());
        }

        public override void Tick()
        {
            base.Tick();

            if (_robot.toAttack.Count > 0) { _currentPlayer = _robot.toAttack.ElementAt(0); }
            else { _currentPlayer = null; }

            if (_currentPlayer != null)
            {
                _robot.transform.DOLookAt(_currentPlayer.transform.position, 2);
                TryShoot();
            }
            else
            {
                _nextState = _findPlayerState;
            }
        }

        private void TryShoot()
        {
            if (_canShoot == false) { return; };

            Vector3 playerPositon = _currentPlayer.transform.position;

            _robot.transform.DOLookAt(playerPositon, 0.25f);
            _shootPosition.LookAt(playerPositon + Vector3.up * transform.position.y);

            var bullet = _bulletPooling.Get(_shootPosition.position, _shootPosition.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _bulletSpeed;
            bullet.Inititlize(_bulletPooling);

            bullet.SetDamage(_damage);

            _canShoot = false;
            StartCoroutine(ResetCanShoot());
        }

        private IEnumerator ResetCanShoot()
        {
            yield return new WaitForSecondsRealtime(_shootRate);
            _canShoot = true;
        }
    }
}