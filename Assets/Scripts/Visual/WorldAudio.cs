using UnityEngine;
namespace Visual
{
    public sealed class WorldAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _music;
        private AudioSource _audioSource;

        private void Start()
        {
            RandomSound();
        }

        private void Update()
        {
            if (_audioSource != null && !_audioSource.isPlaying)
            {
                RandomSound();
            }
        }

        private void RandomSound()
        {
            var index = Random.Range(0, _music.Length);
            _audioSource = _music[index];
            _audioSource.Play();
        }
    }
}
