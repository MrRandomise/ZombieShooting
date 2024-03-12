using Atomic;
using Core;
using UnityEngine;

namespace Logics
{
    public sealed class BulletCollision
    {
        private readonly AtomicVariable<int> _damage;
        private readonly AtomicEvent _onDeath;

        public BulletCollision(AtomicVariable<int> damage, AtomicEvent onDeath)
        {
            _damage = damage;
            _onDeath = onDeath;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICharacter character))
            {
                character.OnTakeDamage.Invoke(_damage.Value);
            }
            _onDeath?.Invoke();
        }
    }
}