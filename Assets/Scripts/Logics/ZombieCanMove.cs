using Atomic;

namespace Logics
{
    public sealed class ZombieCanMove
    {
        private readonly AtomicVariable<bool> _isAlive;
        private readonly AtomicVariable<bool> _canMove;
        private readonly AtomicVariable<bool> _isAttacking;
        private readonly AtomicVariable<bool> _attackDistanceReached;

        public ZombieCanMove(AtomicVariable<bool> isAlive, AtomicVariable<bool> canMove, AtomicVariable<bool> isAttacking, AtomicVariable<bool> attackDistanceReached)
        {
            _isAlive = isAlive;
            _canMove = canMove;
            _attackDistanceReached = attackDistanceReached;
            _isAttacking = isAttacking;
        }

        public void Update()
        {
            _canMove.Value = _isAlive.Value && !_attackDistanceReached.Value && !_isAttacking.Value;
        }
    }
}
