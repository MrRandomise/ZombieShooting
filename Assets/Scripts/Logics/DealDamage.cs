using Atomic;
using Core;
using UnityEngine;

namespace Logics
{
    public sealed class DealDamage : IEventLogics
    {
        private readonly Transform _targetTransform;
        private readonly AtomicVariable<int> _damage;
        private readonly AtomicEvent _onHit;

        public DealDamage(Transform targetTransform, AtomicVariable<int> damage, AtomicEvent onHit)
        {
            _targetTransform = targetTransform;
            _damage = damage;
            _onHit = onHit;
        }


        public void OnEnable()
        {
            _onHit.Subscribe(DealDamageLogic);
        }

        public void OnDisable()
        {
            _onHit.UnSubscribe(DealDamageLogic);
        }

        private void DealDamageLogic()
        {
            _targetTransform.GetComponent<ICharacter>().OnTakeDamage.Invoke(_damage.Value);
        }
    }
}
