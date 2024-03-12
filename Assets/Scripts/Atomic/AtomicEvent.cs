using System;

namespace Atomic
{
    [Serializable]
    public sealed class AtomicEvent<T>
    {
        private event Action<T> _onEvent;

        public void Invoke(T args)
        {
            _onEvent?.Invoke(args);
        }

        public void Subscribe(Action<T> action)
        {
            _onEvent += action;
        }

        public void UnSubscribe(Action<T> action)
        {
            _onEvent -= action;
        }
    }
    
    [Serializable]
    public sealed class AtomicEvent
    {
        private event Action _onEvent;

        public void Invoke()
        {
            _onEvent?.Invoke();
        }

        public void Subscribe(Action action)
        {
            _onEvent += action;
        }

        public void UnSubscribe(Action action)
        {
            _onEvent -= action;
        }
    }
}
