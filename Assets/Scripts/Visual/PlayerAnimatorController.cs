using Atomic;
using UnityEngine;

namespace Visual
{
    public sealed class PlayerAnimatorController
    {
        private readonly AtomicVariable<Vector3> _moveDirection;
        private bool _isAlive;
        private readonly AtomicVariable<bool> _canMove;
        private readonly AtomicEvent _onDeath;
        private readonly Animator _animator;
        private readonly AtomicEvent _onReload;

        public PlayerAnimatorController(AtomicVariable<Vector3> moveDirection, AtomicVariable<bool> canMove, Animator animator, AtomicEvent onDeath, AtomicEvent onReload)
        {
            _moveDirection = moveDirection;
            _canMove = canMove;
            _onDeath = onDeath;
            _animator = animator;
            _onReload = onReload;
            _isAlive = true;
        }

        private void DieAnimator()
        {
            _animator.SetTrigger("Death");
            _isAlive = false;
        }

        private void ReloadAnimator()
        {
            _animator.SetTrigger("Reload");
        }

        private int GetMainStateValue()
        {
            if (_moveDirection.Value == Vector3.zero || !_canMove.Value)
                return 0;
            return 1;
        }

        public void Update()
        {
            if (!_isAlive) return;
            _animator.SetInteger("Move", GetMainStateValue());
        }

        public void OnEnable()
        {
            _onDeath.Subscribe(DieAnimator);
            _onReload.Subscribe(ReloadAnimator);
        }

        public void OnDisable()
        {
            _onDeath.UnSubscribe(DieAnimator);
            _onReload.UnSubscribe(ReloadAnimator);
        }
    }
}
