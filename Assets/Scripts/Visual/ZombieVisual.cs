using UnityEngine;
using ZombieModel;

namespace Visual
{
    public sealed class ZombieVisual: MonoBehaviour
    {
        [SerializeField] private Zombie _zombie;
        [SerializeField] private Animator animator;
        private ZombieAnimatorController _zombieAnimatorController;

        private void Awake()
        {
            _zombieAnimatorController = new ZombieAnimatorController(_zombie, animator);
        }

        private void OnEnable()
        {
            _zombieAnimatorController.OnEnable();
        }

        private void OnDisable()
        {
            _zombieAnimatorController.OnDisable();
        }

        private void Update()
        {
            _zombieAnimatorController.Update();
        }

    }
}
