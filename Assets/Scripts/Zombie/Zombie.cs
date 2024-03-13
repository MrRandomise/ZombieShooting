using System.Collections.Generic;
using Core;
using Atomic;
using Logics;
using UnityEngine;
using Visual;

namespace ZombieModel
{
    public class Zombie : MonoBehaviour, ICharacter
    {
        public Transform PlayerTransform;
        [SerializeField] private ZombieHands _rightHand;
        [SerializeField] private ZombieHands _leftHand;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;

        public AtomicVariable<int> Health;
        public AtomicVariable<int> Damage;
        public AtomicVariable<bool> IsAlive;
        public AtomicVariable<float> Speed;
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
        
        private AtomicEvent _rightHandCollision;
        private AtomicEvent _leftHandCollision;
        private readonly List<IEventLogics> _logics = new();
        private Movement _movement;
        private ZombieAttack _attack;
        private TargetMovement _targetMovement;
        private ZombieCanMove _zombieCanMove;
        private CheckAttackDistance _attackDistanceCheck;
        private ZombieCanAttack _canAttack;
        private DecayInactiveObject _decayInactiveObject;


        private void Awake()
        {
            PlayerTransform = GameObject.FindWithTag("Player").transform;
            _rightHandCollision = _rightHand._collision;
            _leftHandCollision = _leftHand._collision;
            _attackDistanceCheck = new CheckAttackDistance(AttackDistance, IsAlive,
                AttackDistanceReached, PlayerTransform, transform);
            _canAttack = new ZombieCanAttack(IsAlive, CanAttack, AttackDistanceReached);
            _movement = new Movement(CanMove, Speed, MoveDirection, transform);
            _targetMovement = new TargetMovement(PlayerTransform, MoveDirection, transform, IsAlive);
            _zombieCanMove = new ZombieCanMove(IsAlive, CanMove, IsAttacking, AttackDistanceReached);
            _attack = new ZombieAttack(CanAttack, IsAttacking, OnAttackRequested);
            _logics.Add(new TakeDamageEvent(Health, OnTakeDamage));
            _logics.Add(new DeathEvent(Health, IsAlive, OnDeath));
            _logics.Add(new DealDamage(PlayerTransform, Damage, OnHit));
            _logics.Add(new ZombieAttackHit(CanAttack, OnHit, _leftHandCollision, _rightHandCollision, IsAttacking, _animatorDispatcher));
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
