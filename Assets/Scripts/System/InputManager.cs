using UnityEngine;
using Zenject;

namespace System
{
    public enum UseKey
    {
        Left,
        Right,
        Forward,
        Backward,
        Stop,
        Fire
    }

    public sealed class InputManager : ITickable
    {
        public Action<UseKey> OnUseKey;

        public void Tick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnUseKey?.Invoke(UseKey.Fire);
            }

            if (Input.GetKey(KeyCode.A))
            {
                OnUseKey?.Invoke(UseKey.Left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                OnUseKey?.Invoke(UseKey.Right);
            }else if (Input.GetKey(KeyCode.W))
            {
                OnUseKey?.Invoke(UseKey.Forward);
            }else if (Input.GetKey(KeyCode.S))
            {
                OnUseKey?.Invoke(UseKey.Backward);
            }else
            {
                OnUseKey?.Invoke(UseKey.Stop);
            }
        }
    }
}