using Atomic;

namespace Logics
{
    public sealed class DeathEvent : IEventLogics
    {
        private readonly AtomicVariable<int> _health;
        private readonly AtomicVariable<bool> _isAlive;
        private readonly AtomicEvent _onDeath;

        public DeathEvent(AtomicVariable<int> health, AtomicVariable<bool> isAlive, AtomicEvent onDeath)
        {
            _health = health;
            _isAlive = isAlive;
            _onDeath = onDeath;
        }

        public void OnEnable()
        {
            _health.Subscribe(Die); 
        }

        public void OnDisable()
        {
            _health.UnSubscribe(Die); 
        }

        private void Die(int value)
        {
            if (value == 0 && _isAlive.Value)
            {
                _isAlive.Value = false;
                _onDeath?.Invoke();
            }
        }
    }
}