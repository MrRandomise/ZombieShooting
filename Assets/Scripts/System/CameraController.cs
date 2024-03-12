using CharacterModel;
using UnityEngine;
using Zenject;

namespace Core
{
    public sealed class CameraController : ITickable
    {
        private readonly Camera _camera;
        private readonly Character _character;
        private readonly Vector3 _initPos;

        public CameraController(Camera camera, Character playerModel)
        {
            _camera = camera;
            _character = playerModel;
            _initPos = camera.transform.position;
        }

        public void Tick()
        {
            _camera.transform.position = _character.transform.position + _initPos;
        }
    }
}
