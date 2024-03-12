using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class TakeDamageEvent : IEventLogics
    {
        private readonly AtomicVariable<int> _health;
        private readonly AtomicEvent<int> _onTakeDamage;

        public TakeDamageEvent(AtomicVariable<int> health, AtomicEvent<int> onTakeDamage)
        {
            _health = health;
            _onTakeDamage = onTakeDamage;
        }

        private void TakeDamage(int damage)
        {
            _health.Value = Mathf.Max(0, _health.Value - damage);
        }

        public void OnEnable()
        {
            _onTakeDamage.Subscribe(TakeDamage);
        }
        
        public void OnDisable()
        {
            _onTakeDamage.UnSubscribe(TakeDamage);
        }
    }
}