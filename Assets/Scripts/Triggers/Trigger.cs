using UnityEngine;
using UnityEngine.Events;

namespace Agava.IdleGame
{
    [RequireComponent(typeof(Collider))]
    public class Trigger : MonoBehaviour 
    {
        public event UnityAction Enter;
        public event UnityAction Stay;
        public event UnityAction Exit;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerCollision playerCollision))
            {
                Enter?.Invoke();
            }
        }

        private void OnTriggerStay(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerCollision playerCollision))
            {
                Stay?.Invoke();
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerCollision playerCollision))
            {
                Exit?.Invoke();
            }
        }
    }
}