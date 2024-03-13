using System.Collections.Generic;
using Core;
using Atomic;
using Logics;
using UnityEngine;

namespace ZombieModel
{
    public class Zombie : MonoBehaviour, ICharacter
    {
        public Transform PlayerTransform;
        public AtomicVariable<int> Health;
        public AtomicVariable<int> Damage;
        public AtomicVariable<bool> IsAlive;
        public AtomicVariable<float> Speed;
        public AtomicVariable<float> AttackTimeout;
        public AtomicVariable<float> AttackDistance;
        public AtomicVariable<bool> AttackDistanceReached;
        public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<bool> CanMove;
        public AtomicVariable<bool> CanAttack;
        public AtomicVariable<bool> IsAttacking;
        public AtomicVariable<float> DecayTimeout;
        public AtomicEvent<int> OnTakeDamage { get; } = new();
        public AtomicEvent OnDeath;
        public AtomicEvent<Zombie> UpdateKillCount;
        public AtomicEvent OnAttackRequested;
        public AtomicEvent OnHit;
        public AtomicEvent DeleteMe;

        private readonly List<IEventLogics> _logics = new();
        private Movement _movement;
        private TargetMovement _targetMovement;
        private ZombieCanMove _zombieCanMove;
        private ZombieAttack _attack;
        private CheckAttackDistance _attackDistanceCheck;
        private ZombieCanAttack _canAttack;
        private DecayInactiveObject _decayInactiveObject;

        private void Awake()
        {
            PlayerTransform = GameObject.FindWithTag("Player").transform;
            var zombyTransform = transform;
            _attackDistanceCheck = new CheckAttackDistance(AttackDistance, IsAlive,
                AttackDistanceReached, PlayerTransform, zombyTransform);
            _canAttack = new ZombieCanAttack(IsAlive, CanAttack, AttackDistanceReached);
            _movement = new Movement(CanMove, Speed, MoveDirection, zombyTransform);
            _targetMovement = new TargetMovement(PlayerTransform, MoveDirection, zombyTransform, IsAlive);
            _zombieCanMove = new ZombieCanMove(IsAlive, CanMove, IsAttacking, AttackDistanceReached);
            _attack = new ZombieAttack(CanAttack, IsAttacking, AttackTimeout, OnAttackRequested, OnHit);
            _logics.Add(new TakeDamageEvent(Health, OnTakeDamage));
            _logics.Add(new DeathEvent(Health, IsAlive, OnDeath));
            _logics.Add(new DealDamage(PlayerTransform, Damage, OnHit));
            _decayInactiveObject = new DecayInactiveObject(DecayTimeout, DeleteMe, OnDeath);
            _logics.Add(_decayInactiveObject);
            _logics.Add(new DeleteObject(DeleteMe, transform));
            _logics.Add(new ZombieKillEvent(this));
            IsAlive.Value = true;
        }

        private void Update()
        {
            _attackDistanceCheck.Update();
            _canAttack.Update();
            _attack.Update();
            _zombieCanMove.Update();
            _targetMovement.Update();
            _movement.Update();
            _decayInactiveObject.Update();
        }

        private void OnEnable()
        {
            foreach (var mechanic in _logics)
            {
                mechanic.OnEnable();
            }
        }

        private void OnDisable()
        {
            foreach (var mechanic in _logics)
            {
                mechanic.OnDisable();
            }
        }
    }
}
