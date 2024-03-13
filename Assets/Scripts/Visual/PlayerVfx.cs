using CharacterModel;
using UnityEngine;

namespace Visual
{
    public sealed class PlayerVfx : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private ParticleSystem _takeDamageParticle;
        [SerializeField] private ParticleSystem _fireParticle;
        public void OnEnable()
        {
            _character.OnFire.Subscribe(OnFire);
            _character.OnTakeDamage.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            _character.OnFire.UnSubscribe(OnFire);
            _character.OnTakeDamage.UnSubscribe(OnTakeDamage);
        }

        private void OnFire()
        {
            _fireParticle.transform.position = _character.FirePoint.position;
            _fireParticle.transform.rotation = _character.FirePoint.rotation;
            _fireParticle.Play();
        }

        private void OnTakeDamage(int damage)
        {
            _takeDamageParticle.Play();
        }
    }
}
