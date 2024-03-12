using UnityEngine;
using ZombieModel;

namespace Visual
{
    public sealed class ZombieVfx : MonoBehaviour
    {
        [SerializeField] private Zombie _zombie;
        [SerializeField] private ParticleSystem _takeDamageParticle;
        public void OnEnable()
        {
            _zombie.OnTakeDamage.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            _zombie.OnTakeDamage.UnSubscribe(OnTakeDamage);
        }



        private void OnTakeDamage(int damage)
        {
            _takeDamageParticle.Play();
        }
    }
}
