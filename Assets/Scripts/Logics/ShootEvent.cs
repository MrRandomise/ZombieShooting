using Atomic;
using UnityEngine;
using BulletCore;

namespace Logics
{
    public sealed class ShootEvent : IEventLogics
    {
        private readonly AtomicEvent _onFireRequested;
        private readonly Transform _firePoint;
        private readonly Bullet _bullet;
        private readonly Transform _transform;
        private readonly AtomicVariable<bool> _canShoot;
        private readonly AtomicEvent _onFire;

        public ShootEvent(AtomicEvent onFireRequested, Transform firePoint, Bullet bullet, Transform transform, AtomicVariable<bool> canShoot, AtomicEvent onFire)
        {
            _onFireRequested = onFireRequested;
            _firePoint = firePoint;
            _bullet = bullet;
            _transform = transform;
            _canShoot = canShoot;
            _onFire = onFire;
        }

        private void OnFireRequested()
        {
            if(!_canShoot.Value)
                return;
            var bulletInstance = Object.Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
            bulletInstance.MoveDirection.Value = _transform.forward;
            _onFire?.Invoke();
        }

        public void OnEnable()
        {
            _onFireRequested.Subscribe(OnFireRequested);
        }

        public void OnDisable()
        {
            _onFireRequested.UnSubscribe(OnFireRequested);
        }
    }
}