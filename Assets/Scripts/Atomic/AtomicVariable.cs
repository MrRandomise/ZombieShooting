using System;
using UnityEngine;

namespace Atomic
{
    [Serializable]
    public sealed class AtomicVariable<T>
    {
        [SerializeField] private T _value;

        private event Action<T> _onValueChanged;
        
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _onValueChanged?.Invoke(value);
            } 
        }

        public void Subscribe(Action<T> action)
        {
            _onValueChanged += action;
        }
        
        public void UnSubscribe(Action<T> action)
        {
            _onValueChanged -= action;
        }
    }
}
