using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class CanShootEvent : IEventLogics
    {
        private readonly AtomicVariable<bool> _canShoot;
        private readonly AtomicVariable<int> _amoAmount;
        private readonly AtomicVariable<float> _shootTimeout;
        private readonly AtomicVariable<bool> _isAlive;
        private readonly AtomicEvent _onFire;
        private float _timeout;
        
        public CanShootEvent(AtomicVariable<bool> canShoot, AtomicVariable<int> amoAmount, AtomicVariable<float> shootTimeout, AtomicEvent onFire, AtomicVariable<bool> isAlive)
        {
            _canShoot = canShoot;
            _amoAmount = amoAmount;
            _shootTimeout = shootTimeout;
            _onFire = onFire;
            _isAlive = isAlive;
        }

        public void Update()
        {
            _timeout -= Time.deltaTime;
            if (!_isAlive.Value || _amoAmount.Value == 0)
            {
                _canShoot.Value = false;
                return;
            }
            if (_timeout < 0)
            {
                _canShoot.Value = true;
                _timeout = _shootTimeout.Value;
            }
        }

        private void ResetTimeout()
        {
            _timeout = _shootTimeout.Value;
            _amoAmount.Value--;
            _canShoot.Value = false;
        }

        public void OnEnable()
        {
            _onFire.Subscribe(ResetTimeout);
        }

        public void OnDisable()
        {
            _onFire.UnSubscribe(ResetTimeout);
        }
    }
}