using CharacterModel;
using UnityEngine;

namespace Visual
{
    public sealed class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _fireAudio;
        [SerializeField] private AudioSource _stepAudio;
        [SerializeField] private AudioSource _takeDamageAudio;
        [SerializeField] private AudioSource _deathAudio;
        [SerializeField] private AudioSource _reloadAmoAudio;
        [SerializeField] private int _reloadAmoSoundAt = 5;
        
        [SerializeField] private Character _character;
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;
        private int reloadAmoCounter;

        public void OnEnable()
        {
            _character.OnFire.Subscribe(OnFire);
            _character.OnReloadAmo.Subscribe(OnReloadAmo);
            _animatorDispatcher.OnEventReceived += OnAnimatorEvent;
            _character.OnDeath.Subscribe(OnDeath);
            _character.OnTakeDamage.Subscribe(OnTakeDamage);
        }

        public void OnDisable()
        {
            _character.OnFire.UnSubscribe(OnFire);
            _character.OnReloadAmo.UnSubscribe(OnReloadAmo);
            _animatorDispatcher.OnEventReceived -= OnAnimatorEvent;
            _character.OnDeath.UnSubscribe(OnDeath);
            _character.OnTakeDamage.Subscribe(OnTakeDamage);
        }

        private void OnFire()
        {
            _fireAudio.Play();
        }

        private void OnReloadAmo()
        {
            if (reloadAmoCounter < 0)
            {
                _reloadAmoAudio.Play();
                reloadAmoCounter = _reloadAmoSoundAt;
                return;
            }

            reloadAmoCounter--;
        }

        private void OnAnimatorEvent(string eventName)
        {
            if (eventName == "step")
            {
                _stepAudio.Play();
            }
        }

        private void OnDeath()
        {
            _deathAudio.Play();
        }

        private void OnTakeDamage(int _)
        {
            if(_character.IsAlive.Value) _takeDamageAudio.Play();
        }
    }
}
