using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class CanSpawn
    {
        private readonly AtomicVariable<float> _spawnTimeout;
        private readonly AtomicVariable<bool> _canSpawn;
        private float _currentTimeout;

        public CanSpawn(AtomicVariable<float> spawnTimeout, AtomicVariable<bool> canSpawn)
        {
            _spawnTimeout = spawnTimeout;
            _canSpawn = canSpawn;
        }

        public void Update()
        {
            _currentTimeout -= Time.deltaTime;
            if (_currentTimeout < 0)
            {
                _currentTimeout = _spawnTimeout.Value;
                _canSpawn.Value = true;
            }
        }
    }
}
