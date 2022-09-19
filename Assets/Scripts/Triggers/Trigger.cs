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

        private PlayerCollision _playerCollision;
        private AssistantCollision _assistantCollision;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerCollision playerCollision) && _playerCollision == null && _assistantCollision == null)
            {
                _playerCollision = playerCollision;
                Enter?.Invoke();
            }

            if (collider.TryGetComponent(out AssistantCollision assistantCollision) && _playerCollision == null && _assistantCollision == null)
            {
                _assistantCollision = assistantCollision;
                Enter?.Invoke();
            }
        }

        private void OnTriggerStay(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerCollision playerCollision) && _assistantCollision == null)
            {
                if (_playerCollision == playerCollision)
                    Stay?.Invoke();
            }

            if (collider.TryGetComponent(out AssistantCollision assistantCollision) && _playerCollision == null)
            {
                if (_assistantCollision == assistantCollision)
                    Stay?.Invoke();
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerCollision playerCollision))
            {
                if (_playerCollision == playerCollision)
                {
                    _playerCollision = null;
                    Exit?.Invoke();
                }
            }

            if (collider.TryGetComponent(out AssistantCollision assistantCollision))
            {
                if (_assistantCollision == assistantCollision)
                {
                    _assistantCollision = null;
                    Exit?.Invoke();
                }
            }
        }
    }
}