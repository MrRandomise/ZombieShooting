using UnityEngine;
using Atomic;

public class ZombieHands : MonoBehaviour
{
    public AtomicEvent _collision;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _collision?.Invoke();
        }
    }
}
