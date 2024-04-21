using System;

namespace Interfaces
{
    public interface IHealth
    {
        public Action<float> onHealthChanged { get; set; }
        public Action onDie { get; set; }

        public bool isAlive { get; }
        public float currentHealth { get; }
        public float maxHealth { get; }

        public void TakeDamage(float damage);
    }
}