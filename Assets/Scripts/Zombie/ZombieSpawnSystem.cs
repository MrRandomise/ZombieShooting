using System;
using Atomic;
using Logics;
using Mechanics;
using Zenject;
using ZombieModel;

namespace Zomby
{
    public sealed class ZombieSpawnSystem : ITickable, IDisposable
    {
        public readonly AtomicVariable<int> KillsCount = new();
        private readonly AtomicVariable<bool> _canSpawn = new();
        private readonly AtomicEvent<Zombie> _newZomieSpawned = new();

        private readonly CanSpawn _canSpawnMechanic;
        private readonly SpawnObject _spawnObjectMechanic;
        private readonly ManageSpawnedObjects _manageSpawnedObjectsMechanic;

        public ZombieSpawnSystem(ZombieSpawnSystemConfig config)
        {
            _canSpawnMechanic = new CanSpawn(config.SpawnTimeout, _canSpawn);
            _spawnObjectMechanic =
                new SpawnObject(_canSpawn, config.ZombiePrefab, config.ParentTransform, config.SpawnPoints, config.PlayerTransform, _newZomieSpawned);
            _spawnObjectMechanic.OnEnable();
            _manageSpawnedObjectsMechanic = new ManageSpawnedObjects(_newZomieSpawned, KillsCount);
            _manageSpawnedObjectsMechanic.OnEnable();
        }

        public void Tick()
        {
            _canSpawnMechanic.Update();
        }

        public void Dispose()
        {
            _spawnObjectMechanic.OnDisable();
            _manageSpawnedObjectsMechanic.OnDisable();
        }
    }
}
