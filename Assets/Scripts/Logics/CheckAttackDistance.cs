using Atomic;
using UnityEngine;

namespace Logics
{
    public sealed class CheckAttackDistance
    {
        private readonly AtomicVariable<float> _attackDistance;
        private readonly AtomicVariable<bool> _isAlive;
        private readonly AtomicVariable<bool> _attackDistanceReached;
        private readonly Transform _playerTransform;
        private readonly Transform _targetTransform;

        public CheckAttackDistance(AtomicVariable<float> attackDistance, AtomicVariable<bool> isAlive, AtomicVariable<bool> attackDistanceReached, Transform playerTransform, Transform targetTransform)
        {
            _attackDistance = attackDistance;
            _isAlive = isAlive;
            _attackDistanceReached = attackDistanceReached;
            _playerTransform = playerTransform;
            _targetTransform = targetTransform;
        }

        public void Update()
        {
            if (_isAlive.Value && Vector3.Distance(_playerTransform.position, _targetTransform.position) < _attackDistance.Value)
            {
                _attackDistanceReached.Value = true;
                return;
            }

            _attackDistanceReached.Value = false;
        }
    }
}
