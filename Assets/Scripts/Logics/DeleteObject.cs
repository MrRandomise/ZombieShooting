using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class DeleteObject : IEventLogics
    {
        private readonly AtomicEvent _deleteMe;
        private readonly Transform _targetTransform;

        public DeleteObject(AtomicEvent deleteMe, Transform targetTransform)
        {
            _deleteMe = deleteMe;
            _targetTransform = targetTransform;
        }

        private void DestroyObject()
        {
            Object.Destroy(_targetTransform.gameObject);
        }

        public void OnEnable()
        {
            _deleteMe.Subscribe(DestroyObject);
        }

        public void OnDisable()
        {
            _deleteMe.UnSubscribe(DestroyObject);
        }
    }
}
