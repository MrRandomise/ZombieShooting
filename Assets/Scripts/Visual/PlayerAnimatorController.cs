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
        private readonly AnimatorDispatcher _animatorDispatcher;
        private readonly AtomicEvent<int> _onDamage;

        public PlayerAnimatorController(AtomicVariable<Vector3> moveDirection, AtomicVariable<bool> canMove, Animator animator, AtomicEvent onDeath, AtomicEvent onReload, AnimatorDispatcher animatorDispatcher, AtomicEvent<int> onDamage)
        {
            _moveDirection = moveDirection;
            _canMove = canMove;
            _onDeath = onDeath;
            _animator = animator;
            _onReload = onReload;
            _isAlive = true;
            _animatorDispatcher = animatorDispatcher;
            _onDamage = onDamage;
        }

        private void DieAnimator()
        {
            _animator.SetTrigger("Death");
            _animator.SetInteger("Guns", 0);
            _isAlive = false;
        }

        private void ReloadAnimator()
        {
            if (!_animator.GetBool("Reload"))
            {
                _animator.SetBool("Reload", true);
            }   
        }

        private void Hit(int damage)
        {
            _animator.SetTrigger("Hit");
        }

        private void OnAnimatorEvent(string eventName)
        {
            if (eventName == "Reload")
            {
                _animator.SetBool("Reload", false);
            }
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
            _onDamage.Subscribe(Hit);
            _animatorDispatcher.OnEventReceived += OnAnimatorEvent;
        }

        public void OnDisable()
        {
            _onDeath.UnSubscribe(DieAnimator);
            _onReload.UnSubscribe(ReloadAnimator);
            _onDamage.UnSubscribe(Hit);
            _animatorDispatcher.OnEventReceived -= OnAnimatorEvent;
        }
    }
}
