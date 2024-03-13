using System.Collections.Generic;
using Atomic;
using UnityEngine;

namespace ZombieModel
{
    public sealed class ZombieSpawnSystemConfig : MonoBehaviour
    {
        public GameObject ZombiePrefab;
        public List<Transform> SpawnPoints;
        public Transform ParentTransform;
        public AtomicVariable<float> SpawnTimeout;
        public Transform PlayerTransform;
    }
}
