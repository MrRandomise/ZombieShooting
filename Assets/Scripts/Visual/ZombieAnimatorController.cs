using Atomic;
using UnityEngine;
using ZombieModel;

namespace Visual
{
    public sealed class ZombieAnimatorController
    {
        private static readonly int MainState = Animator.StringToHash("State");
        private const int _idle = 0;
        private const int _move = 1;
        private const int _attack = 2;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly AtomicVariable<bool> _isAttacking;
        private bool _isAlive;
        private readonly AtomicVariable<bool> _canMove;
        private readonly AtomicEvent _onDeath;
        private readonly Animator _animator;

        public ZombieAnimatorController(Zombie zombie, Animator animator)
        {
            _moveDirection = zombie.MoveDirection;
            _canMove = zombie.CanMove;
            _onDeath = zombie.OnDeath;
            _animator = animator;
            _isAttacking = zombie.IsAttacking;
            _isAlive = true;
        }

        private void Die()
        {
            _animator.SetTrigger("Death");
            _isAlive = false;
        }

        private int GetMainStateValue()
        {
            if (_isAttacking.Value) return _attack;
            if (_moveDirection.Value == Vector3.zero || !_canMove.Value)
                return _idle;
            return _move;
        }

        public void Update()
        {
            if(!_isAlive) return;
            _animator.SetInteger(MainState, GetMainStateValue());
        }

        public void OnEnable()
        {
            _onDeath.Subscribe(Die);
        }

        public void OnDisable()
        {
            _onDeath.UnSubscribe(Die);
        }
    }
}
