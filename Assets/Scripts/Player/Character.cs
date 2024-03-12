using System.Collections.Generic;
using Atomic;
using Logics;
using BulletCore;
using UnityEngine;
using Core;

namespace CharacterModel
{
    public sealed class Character : MonoBehaviour, ICharacter
    {
        public AtomicVariable<int> Health;
        public AtomicVariable<int> AmoAmount;
        public AtomicVariable<int> MaxAmoAmount;
        public AtomicVariable<bool> IsAlive;
        public AtomicVariable<float> Speed;
        public Transform FirePoint;
        public Bullet Bullet;
        public AtomicVariable<float> ShootTimeout;
        public AtomicVariable<int> AddAmoTimeout;
        public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<bool> CanMove;
        public AtomicVariable<bool> CanShoot;
        public AtomicEvent<int> OnTakeDamage { get; } = new();
        public AtomicEvent OnDeath;
        public AtomicEvent OnFireRequested;
        public AtomicEvent OnFire;
        public AtomicEvent OnReloadAmo;
        
        private readonly List<IEventLogics> _logics = new();
        private Movement _movement;
        private RotateCharacter _rotateCharacter;
        private CanShootEvent _canShootEvent;
        private AddAmo _addAmoMechanic;

        private void Awake()
        {
            _logics.Add(new CharacterCant(CanMove, IsAlive));
            _logics.Add(new TakeDamageEvent(Health, OnTakeDamage));
            _logics.Add(new DeathEvent(Health,IsAlive, OnDeath));
            _logics.Add(new ShootEvent(OnFireRequested, FirePoint, Bullet, transform, CanShoot, OnFire));
            var playerTransform = transform;
            _movement = new Movement(CanMove, Speed, MoveDirection, playerTransform);
            _rotateCharacter = new RotateCharacter(IsAlive, playerTransform);
            _canShootEvent = new CanShootEvent(CanShoot, AmoAmount, ShootTimeout, OnFire, IsAlive);
            _logics.Add(_canShootEvent);
            _addAmoMechanic = new AddAmo(AddAmoTimeout, AmoAmount, IsAlive, OnReloadAmo, MaxAmoAmount);
            _addAmoMechanic.Awake();
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
