using System;
using Core;
using CharacterModel;
using UI;
using UnityEngine;
using Zenject;
using Zomby;

namespace ZInstaller
{
    public sealed class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Character _character;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private ZombieSpawnSystemConfig _zombieSpawnSystemConfig;

        public override void InstallBindings()
        {
            Container.Bind<Character>().FromInstance(_character).AsSingle();
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterInputController>().AsSingle();
            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle();
            Container.Bind<ZombieSpawnSystemConfig>().FromInstance(_zombieSpawnSystemConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<ZombieSpawnSystem>().AsSingle();
        }
    }
}