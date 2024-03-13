using Atomic;
using Unity.VisualScripting;
using Visual;

namespace Logics
{
    public class ZombieAttackHit : IEventLogics
    {
        private readonly AtomicVariable<bool> _canAttack;
        private readonly AtomicEvent _onHit;
        private readonly AtomicEvent _leftHand;
        private readonly AtomicEvent _rightHand;
        private readonly AtomicVariable<bool> _isAttacking;
        private AnimatorDispatcher _animatorDispatcher;

        public ZombieAttackHit(AtomicVariable<bool> canAttack, AtomicEvent onHit, AtomicEvent leftHand, AtomicEvent rightHand, AtomicVariable<bool> isAttacking, AnimatorDispatcher animatorDispatcher)
        {
            _canAttack = canAttack;
            _onHit = onHit;
            _leftHand = leftHand;
            _rightHand = rightHand;
            _isAttacking = isAttacking;
            _animatorDispatcher = animatorDispatcher;
        }

        private void OnAnimatorEvent(string eventName)
        {
            if (eventName == "StopAttack")
            {
                _isAttacking.Value = false;
            }
        }


        private void TryHit()
        {
            if (_canAttack.Value)
            {
                _onHit.Invoke();
            }
        }

        public void OnEnable()
        {
            _leftHand.Subscribe(TryHit);
            _rightHand.Subscribe(TryHit);
            _animatorDispatcher.OnEventReceived += OnAnimatorEvent;
        }

        public void OnDisable()
        {
            _leftHand.UnSubscribe(TryHit);
            _rightHand.UnSubscribe(TryHit);
            _animatorDispatcher.OnEventReceived -= OnAnimatorEvent;
        }
    }
}
