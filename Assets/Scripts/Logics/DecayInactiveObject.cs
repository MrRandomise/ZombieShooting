using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class DecayInactiveObject : IEventLogics
    {
        private readonly AtomicVariable<float> _deleteTimeout;
        private readonly AtomicEvent _deleteMe;
        private readonly AtomicEvent _onDeath;
        private bool _isInactive;
        private float _currentTimeout;

        public DecayInactiveObject(AtomicVariable<float> deleteTimeout, AtomicEvent deleteMe, AtomicEvent onDeath)
        {
            _deleteTimeout = deleteTimeout;
            _deleteMe = deleteMe;
            _onDeath = onDeath;
        }

        private void BeginDecay()
        {
            _isInactive = true;
            _currentTimeout = _deleteTimeout.Value;
        }

        public void Update()
        {
            if(!_isInactive) return;
            _currentTimeout -= Time.deltaTime;
            if (_currentTimeout < 0)
            {
                _deleteMe.Invoke();
            }
        }

        public void OnEnable()
        {
            _onDeath.Subscribe(BeginDecay);
        }

        public void OnDisable()
        {
            _onDeath.UnSubscribe(BeginDecay);
        }
    }
}
