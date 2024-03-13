using Atomic;
using UnityEditor.SceneManagement;
using UnityEngine;
using ZombieModel;

namespace Visual
{
    public sealed class ZombieAnimatorController
    {
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

        private void GetMainStateValue()
        {
            if (_isAttacking.Value) 
                _animator.SetTrigger("Attack");
            if (_moveDirection.Value == Vector3.zero || !_canMove.Value)
                _animator.SetInteger("Move", 0);
            else
                _animator.SetInteger("Move", 1);
        }

        public void Update()
        {
            if(!_isAlive) return;
            GetMainStateValue();
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
