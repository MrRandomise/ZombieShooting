using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class TargetMovement
    {
        private readonly Transform _targetTransform;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly Transform _entityTransform;
        private readonly AtomicVariable<bool> _isAlive;


        public TargetMovement(Transform targetTransform, AtomicVariable<Vector3> moveDirection, Transform entityTransform, AtomicVariable<bool> isAlive)
        {
            _targetTransform = targetTransform;
            _moveDirection = moveDirection;
            _entityTransform = entityTransform;
            _isAlive = isAlive;
        }

        public void Update()
        {
            if (_isAlive.Value)
            {
                var position = _targetTransform.position;
                _entityTransform.LookAt(new Vector3(position.x, _entityTransform.position.y, position.z));
                _moveDirection.Value = _entityTransform.forward;    
            }
        }
    }
}
