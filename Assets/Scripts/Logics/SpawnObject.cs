using System.Collections.Generic;
using Atomic;
using Logics;
using UnityEngine;
using ZombieModel;

namespace Mechanics
{
    public sealed class SpawnObject : IEventLogics
    {
        private readonly AtomicVariable<bool> _canSpawn;
        private readonly GameObject _spawnPrefab;
        private readonly Transform _parentTransform;
        private readonly List<Transform> _spawnPoint;
        private readonly AtomicEvent<Zombie> _spawnEvent;

        public SpawnObject(AtomicVariable<bool> canSpawn, GameObject spawnPrefab, Transform parentTransform, List<Transform> spawnPoint, Transform playerTransform, AtomicEvent<Zombie> spawnEvent)
        {
            _canSpawn = canSpawn;
            _spawnPrefab = spawnPrefab;
            _parentTransform = parentTransform;
            _spawnPoint = spawnPoint;
            _spawnEvent = spawnEvent;
        }

        public void OnEnable()
        {
            _canSpawn.Subscribe(Spawn);
        }

        public void OnDisable()
        {
            _canSpawn.UnSubscribe(Spawn);
        }

        private void Spawn(bool value)
        {
            if (!value) return;
            var instance = Object.Instantiate(_spawnPrefab, GetSpawnPoint(), _spawnPrefab.transform.rotation , _parentTransform);
            _spawnEvent.Invoke(instance.GetComponent<Zombie>());
            _canSpawn.Value = false;
        }

        private Vector3 GetSpawnPoint()
        {
            return _spawnPoint[Random.Range(0, _spawnPoint.Count - 1)].position;
        }
    }
}
