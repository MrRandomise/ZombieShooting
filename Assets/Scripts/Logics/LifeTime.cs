using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class LifeTime
    {
        private float _lifetime;
        private readonly AtomicEvent _onDeath;

        public LifeTime(AtomicVariable<float> lifeTime, AtomicEvent onDeath)
        {
            _lifetime = lifeTime.Value;
            _onDeath = onDeath;
        }

        public void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime < 0)
            {
                _onDeath?.Invoke();
            }
        }
    }
}