using Atomic;
using ZombieModel;

namespace Logics
{
    public sealed class ManageSpawnedObjects : IEventLogics
    {
        private readonly AtomicEvent<Zombie> _zombieSpawnEvent;
        private readonly AtomicVariable<int> _killCount;

        public ManageSpawnedObjects(AtomicEvent<Zombie> zombieSpawnEvent, AtomicVariable<int> killCount)
        {
            _zombieSpawnEvent = zombieSpawnEvent;
            _killCount = killCount;
        }

        private void AddZombie(Zombie zombie)
        {
            zombie.UpdateKillCount.Subscribe(OnZombyDeath);
        }

        private void OnZombyDeath(Zombie zombie)
        {
            zombie.UpdateKillCount.UnSubscribe(AddZombie);
            _killCount.Value++;
        }

        public void OnEnable()
        {
            _zombieSpawnEvent.Subscribe(AddZombie);
        }

        public void OnDisable()
        {
            _zombieSpawnEvent.UnSubscribe(AddZombie);
        }
    }
}
