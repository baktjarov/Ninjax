using Interfaces;
using System;
using UnityEngine;

namespace Characters
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        public Action onDie { get; set; }
        public Action<float> onHealthChanged { get; set; }

        public bool isAlive { get; private set; }
        public float currentHealth { get; private set; } = 100;
        public float maxHealth { get; private set; } = 100;

        private void Start()
        {
            currentHealth = 100;
        }

        private void Update()
        {
            if (!isAlive && currentHealth <= 0)
            {
                isAlive = true;
                onDie?.Invoke();
            }
        }

        public void TakeDamage(float damage)
        {
            currentHealth = currentHealth - damage;

            onHealthChanged?.Invoke(currentHealth);
        }
    }

}