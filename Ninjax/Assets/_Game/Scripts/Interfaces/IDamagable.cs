using System;

namespace Interfaces
{
    public interface IDamagable
    {
        public Action onDie { get; set; }
        public void TakeDamage(float damage);
    }
}