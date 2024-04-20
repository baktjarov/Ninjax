using Interfaces;
using System;
using UnityEngine;

namespace Characters.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamagable
    {
        public Action onDie { get; set; }
        [SerializeField] private float _health;

        private void Start()
        {
            _health = 100;
        }

        private void Update()
        {
            if(_health <= 0)
            {
                onDie?.Invoke();

                Destroy(gameObject);
                return;
            }
        }

        public void TakeDamage(float damage)
        {
            _health = _health - damage;
        }
    }
}