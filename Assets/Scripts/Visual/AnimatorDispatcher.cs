using System;
using UnityEngine;

namespace Visual
{
    public sealed class AnimatorDispatcher : MonoBehaviour
    {
        public event Action<string> OnEventReceived;
        
        public void ReceiveEvent(string eventName)
        {
            OnEventReceived?.Invoke(eventName);
        }
    }
}
