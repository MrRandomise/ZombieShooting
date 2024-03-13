using UnityEngine;
using Atomic;

namespace Logics
{
    public class DropGun : IEventLogics
    {
        private readonly AtomicVariable<GameObject> _weapon;
        private readonly AtomicEvent _onDie;

        public DropGun(AtomicVariable<GameObject> weapon, AtomicEvent onDie)
        {
            _weapon = weapon;
            _onDie = onDie;
        }

        private void Drop()
        {
            _weapon.Value.transform.parent = null;
            _weapon.Value.GetComponent<Rigidbody>().isKinematic = false;
            _weapon.Value.GetComponent<Rigidbody>().useGravity = true;
        }

        public void OnEnable()
        {
            _onDie.Subscribe(Drop);
        }

        public void OnDisable()
        {
            _onDie.UnSubscribe(Drop);
        }
    }
}
