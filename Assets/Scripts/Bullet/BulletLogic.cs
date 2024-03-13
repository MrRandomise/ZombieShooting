using Atomic;
using Logics;
using UnityEngine;

namespace BulletCore
{
    public sealed class BulletLogic : MonoBehaviour
    {
        public AtomicVariable<float> Speed;
        public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<bool> CanMove;
        public AtomicVariable<int> Damage;
        public AtomicVariable<float> LifeTime;
        public AtomicEvent OnDeath;

        private Movement _movementMechanic;
        private BulletCollision _bulletCollisionMechanic;
        private LifeTime _lifeTimeMechanic;
        private DestroyEvent _destroyEventMechanic;

        private void Awake()
        {
            _movementMechanic = new Movement(CanMove, Speed, MoveDirection, transform);
            _bulletCollisionMechanic = new BulletCollision(Damage, OnDeath);
            _lifeTimeMechanic = new LifeTime(LifeTime, OnDeath);
            _destroyEventMechanic = new DestroyEvent(OnDeath, CanMove, gameObject);
        }

        private void OnEnable()
        {
            _destroyEventMechanic.OnEnable();
        }

        private void OnDisable()
        {
            _destroyEventMechanic.OnDisable();
        }

        private void Update()
        {
            _movementMechanic.Update();
            _lifeTimeMechanic.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            _bulletCollisionMechanic.OnTriggerEnter(other);
        }
    }
}
