using Atomic;

namespace Logics
{
    public sealed class ZombieCanAttack
    {
        private readonly AtomicVariable<bool> _isAlive;
        private readonly AtomicVariable<bool> _canAttack;
        private readonly AtomicVariable<bool> _attackDistanceReached;


        public ZombieCanAttack(AtomicVariable<bool> isAlive, AtomicVariable<bool> canAttack, AtomicVariable<bool> attackDistanceReached)
        {
            _isAlive = isAlive;
            _canAttack = canAttack;
            _attackDistanceReached = attackDistanceReached;
        }

        public void Update()
        {
            _canAttack.Value = _isAlive.Value && _attackDistanceReached.Value;
        }
    }
}
