using Interfaces;
using System;
using UnityEngine;

namespace Characters
{
    public class Player : MonoBehaviour, IDamagable
    {
        private bool _isDead = false;
        public Action onDie { get; set; }
        [SerializeField] private float _health;

        private void Start()
        {
            _health = 100;
        }

        private void Update()
        {
            if (!_isDead && _health <= 0)
            {
                _isDead = true;
                onDie?.Invoke();
            }
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
        }
    }

}