using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class Attack
    {
        private readonly AtomicVariable<bool> _canAttack;
        private readonly AtomicVariable<bool> _isAttacking;
        private readonly AtomicVariable<float> _attackTimeout;
        private readonly AtomicEvent _onAttackRequest;
        private readonly AtomicEvent _onHit;
        private float _currentTimeout;


        public Attack(AtomicVariable<bool> canAttack, AtomicVariable<bool> isAttacking, AtomicVariable<float> attackTimeout, AtomicEvent onAttackRequest, AtomicEvent onHit)
        {
            _canAttack = canAttack;
            _isAttacking = isAttacking;
            _attackTimeout = attackTimeout;
            _onAttackRequest = onAttackRequest;
            _onHit = onHit;
        }

        public void Update()
        {
            if (_canAttack.Value && !_isAttacking.Value)
            {
                _isAttacking.Value = true;
                _currentTimeout = _attackTimeout.Value;
                _onAttackRequest.Invoke();
                return;
            }
            
            if (_isAttacking.Value)
            {
                _currentTimeout -= Time.deltaTime;
                if (_currentTimeout < 0)
                {
                    TryHit();
                }
            }
        }

        private void TryHit()
        {
            if (_canAttack.Value)
            {
                _onHit.Invoke();
            }

            _isAttacking.Value = false;
        }
    }
}
