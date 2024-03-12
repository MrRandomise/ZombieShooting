using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class Rotate
    {
        private const float _rotateSpeed = 20f;
        private readonly AtomicVariable<bool> _isAlive;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly Transform _targetTransform;

        public Rotate(AtomicVariable<bool> isAlive, AtomicVariable<Vector3> moveDirection, Transform transform)
        {
            _isAlive = isAlive;
            _moveDirection = moveDirection;
            _targetTransform = transform;
        }

        public void Update()
        {
            if(!_isAlive.Value || _moveDirection.Value == Vector3.zero) return;
            var rotation = Quaternion.LookRotation(_moveDirection.Value);
            _targetTransform.rotation = Quaternion.Lerp(_targetTransform.rotation, rotation, Time.deltaTime * _rotateSpeed);
        }
    }
}
