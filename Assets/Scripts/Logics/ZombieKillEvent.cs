using ZombieModel;

namespace Logics
{
    public sealed class ZombieKillEvent : IEventLogics
    {
        private readonly Zombie _zombie;

        public ZombieKillEvent(Zombie zombie)
        {
            _zombie = zombie;
        }

        private void OnDeath()
        {
            _zombie.UpdateKillCount.Invoke(_zombie);
        }

        public void OnEnable()
        {
            _zombie.OnDeath.Subscribe(OnDeath);
        }

        public void OnDisable()
        {
            _zombie.OnDeath.UnSubscribe(OnDeath);
        }
    }
}
