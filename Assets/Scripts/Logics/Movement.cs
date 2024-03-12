using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class Movement
    {
        private readonly AtomicVariable<bool> _canMove;
        private readonly AtomicVariable<float> _speed;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly Transform _targetTransform;

        public Movement(AtomicVariable<bool> canMove, AtomicVariable<float> speed, AtomicVariable<Vector3> moveDirection, Transform transform)
        {
            _canMove = canMove;
            _speed = speed;
            _moveDirection = moveDirection;
            _targetTransform = transform;
        }

        public void Update()
        {
            if(!_canMove.Value) return;
            _targetTransform.position += _speed.Value * Time.deltaTime * _moveDirection.Value;
        }
    }
}