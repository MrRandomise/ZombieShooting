using UnityEngine;
using ZombieModel;

namespace Visual
{
    public sealed class ZombieAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _startAudio;
        [SerializeField] private AudioSource _attackAudio;
        [SerializeField] private AudioSource _deathAudio;
        [SerializeField] private Zombie _zombie;

        private void OnEnable()
        {
            _startAudio.Play();
            _zombie.OnDeath.Subscribe(OnDeath);
            _zombie.OnHit.Subscribe(OnHit);
        }

        private void OnDisable()
        {
            _zombie.OnDeath.UnSubscribe(OnDeath);
            _zombie.OnHit.UnSubscribe(OnHit);
        }

        private void OnDeath()
        {
            _deathAudio.Play();
        }
        

        private void OnHit()
        {
            _attackAudio.Play();
        }
    }
}
