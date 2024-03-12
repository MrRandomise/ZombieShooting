using CharacterModel;
using UnityEngine;

namespace Visual
{
    public sealed class CharacterVisual : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Animator _animator;
        private PlayerAnimatorController _characterAnimatorController;

        private void Awake()
        {
            _characterAnimatorController = new PlayerAnimatorController(_character.MoveDirection, _character.CanMove, _animator,
                _character.OnDeath);
        }

        private void OnEnable()
        {
            _characterAnimatorController.OnEnable();
        }

        private void OnDisable()
        {
            _characterAnimatorController.OnDisable();
        }

        private void Update()
        {
            _characterAnimatorController.Update();
        }
    }
}
