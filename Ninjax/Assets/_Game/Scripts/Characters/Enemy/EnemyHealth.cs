using Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public Action onDie { get; set; }
        public Action<float> onHealthChanged { get; set; }

        public bool isAlive { get; private set; }
        public float currentHealth { get; private set; } = 100;
        public float maxHealth { get; private set; } = 100;

        [SerializeField] private Slider _healthSlider;

        private void Start()
        {
            if (_healthSlider == null) { _healthSlider = GetComponentInChildren<Slider>(); }
            if (currentHealth < maxHealth) { currentHealth = maxHealth; }

            _healthSlider.value = currentHealth;
            _healthSlider.maxValue = maxHealth;
        }

        private void Update()
        {
            if (currentHealth <= 0)
            {
                onDie?.Invoke();

                Destroy(gameObject);
                return;
            }
        }

        public void TakeDamage(float damage)
        {
            currentHealth = currentHealth - damage;

            onHealthChanged?.Invoke(currentHealth);

            _healthSlider.value = currentHealth;
        }
    }
}