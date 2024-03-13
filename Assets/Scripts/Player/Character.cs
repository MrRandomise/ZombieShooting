using System.Collections.Generic;
using Atomic;
using Logics;
using BulletCore;
using UnityEngine;
using Core;
using Visual;

namespace CharacterModel
{
    public sealed class Character : MonoBehaviour, ICharacter
    {
        public AtomicVariable<GameObject> _weapon;
        public AtomicVariable<int> Health;
        public AtomicVariable<int> AmoAmount;
        public AtomicVariable<int> MaxAmoAmount;
        public AtomicVariable<bool> IsAlive;
        public AtomicVariable<float> Speed;
        public Transform FirePoint;
        public BulletLogic Bullet;
        public AtomicVariable<float> ShootTimeout;
        public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<bool> CanMove;
        public AtomicVariable<bool> CanShoot;
        public AtomicEvent<int> OnTakeDamage { get; } = new();
        public AtomicEvent OnDeath;
        public AtomicEvent OnFireRequested;
        public AtomicEvent OnFire;
        public AtomicEvent OnReloadAmo;
        public AnimatorDispatcher animatorDispatcher;

        private readonly List<IEventLogics> _logics = new();
        private Movement _movement;
        private RotateCharacter _rotateCharacter;
        private CharacterCanShootEvent _canShootEvent;

        private void Awake()
        {
            _logics.Add(new CharacterCanMove(CanMove, IsAlive));
            _logics.Add(new TakeDamageEvent(Health, OnTakeDamage));
            _logics.Add(new DeathEvent(Health,IsAlive, OnDeath));
            _logics.Add(new CharacterShootEvent(OnFireRequested, FirePoint, Bullet, CanShoot, OnFire));
            _logics.Add(new RealoadAmo(AmoAmount, IsAlive, OnReloadAmo, OnFireRequested, MaxAmoAmount, animatorDispatcher));
            _logics.Add(new DropGun(_weapon, OnDeath));
            var playerTransform = transform;
            _movement = new Movement(CanMove, Speed, MoveDirection, playerTransform);
            _rotateCharacter = new RotateCharacter(IsAlive, playerTransform);
            _canShootEvent = new CharacterCanShootEvent(CanShoot, AmoAmount, ShootTimeout, OnFire, IsAlive);
            _logics.Add(_canShootEvent);
            IsAlive.Value = true;
        }
        
        private void Update()
        {
            _movement.Update();
            _rotateCharacter.Update();
            _canShootEvent.Update();
        }

        private void OnEnable()
        {
            foreach (var obj in _logics)
            {
                obj.OnEnable();
            }
        }

        private void OnDisable()
        {
            foreach (var obj in _logics)
            {
                obj.OnDisable();
            }
        }
    }
}
