using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class DestroyEvent : IEventLogics
    {
        private readonly AtomicEvent _onDeath;
        private readonly AtomicVariable<bool> _canMove;
        private readonly GameObject _gameObject;

        public DestroyEvent(AtomicEvent onDeath, AtomicVariable<bool> canMove, GameObject gameObject)
        {
            _onDeath = onDeath;
            _canMove = canMove;
            _gameObject = gameObject;
        }

        private void OnDeath()
        {
            if (!_canMove.Value)
            {
                return;
            }

            _canMove.Value = false;
            Object.Destroy(_gameObject);
        }

        public void OnEnable()
        {
            _onDeath.Subscribe(OnDeath);
        }

        public void OnDisable()
        {
            _onDeath.UnSubscribe(OnDeath);
        }
    }
}