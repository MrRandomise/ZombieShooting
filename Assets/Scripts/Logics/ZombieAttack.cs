using Atomic;

namespace Logics
{
    public sealed class ZombieAttack
    {
        private readonly AtomicVariable<bool> _canAttack;
        private readonly AtomicVariable<bool> _isAttacking;
        private readonly AtomicEvent _onAttackRequest;


        public ZombieAttack(AtomicVariable<bool> canAttack, AtomicVariable<bool> isAttacking,  AtomicEvent onAttackRequest)
        {
            _canAttack = canAttack;
            _isAttacking = isAttacking;
            _onAttackRequest = onAttackRequest;
        }

        public void Update()
        {
            if (_canAttack.Value && !_isAttacking.Value)
            {
                _isAttacking.Value = true;
                _onAttackRequest.Invoke();
                return;
            }
        }
    }
}
