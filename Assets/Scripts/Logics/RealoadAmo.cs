using Atomic;
using Visual;

namespace Logics
{
    public sealed class RealoadAmo : IEventLogics
    {
        private readonly AtomicVariable<int> _amoAmount;
        private readonly AtomicVariable<int> _maxbullet;
        private readonly AtomicVariable<bool> _isAlive;
        private readonly AtomicEvent _onFire;
        private readonly AtomicEvent _reloadAmo;
        private readonly AnimatorDispatcher _animatorDispatcher;
        public RealoadAmo(AtomicVariable<int> amoAmount, AtomicVariable<bool> isAlive, AtomicEvent reloadAmo, AtomicEvent onFire, AtomicVariable<int> maxbullet, AnimatorDispatcher animatorDispatcher)
        {
            _onFire = onFire;
            _amoAmount = amoAmount;
            _isAlive = isAlive;
            _reloadAmo = reloadAmo;
            _maxbullet = maxbullet;
            _animatorDispatcher = animatorDispatcher;
        }

        public void Reload()
        {
            if(_amoAmount.Value == 0)
            {
                _reloadAmo?.Invoke();
            }
        }

        private void OnAnimatorEvent(string eventName)
        {
            if (_isAlive.Value && eventName == "Reload")
            {
                _amoAmount.Value = _maxbullet.Value;
            }
        }

        public void OnEnable()
        {
            _onFire.Subscribe(Reload);
            _animatorDispatcher.OnEventReceived += OnAnimatorEvent;
        }

        public void OnDisable()
        {
            _onFire.UnSubscribe(Reload);
            _animatorDispatcher.OnEventReceived += OnAnimatorEvent;
        }
    }
}
