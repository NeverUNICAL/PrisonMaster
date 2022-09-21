using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ForCreo
{
    [RequireComponent(typeof(BoxCollider))]
    public class DoorButton : MonoBehaviour
    {
        [SerializeField] private Transform _button;
        [SerializeField] private bool _cellsButton = false;

        private bool _isReached = false;
        private PlayerCollision _playerCollision;
        private AssistantCollision _assistantCollision;
        private BoxCollider _boxCollider;

        public bool IsReached => _isReached;

        public event UnityAction Reached;
        public event UnityAction Stay;
        public event UnityAction Exit;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCollision player) && _playerCollision == null && _assistantCollision == null)
            {
                ChangeButtonState(true, Vector3.zero);
                _playerCollision = player;
                Reached?.Invoke();
            }

            if (other.TryGetComponent(out AssistantCollision assistantCollision) && _playerCollision == null && _assistantCollision == null)
            {
                ChangeButtonState(true, Vector3.zero);
                _assistantCollision = assistantCollision;
                Reached?.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out PlayerCollision player) && _playerCollision != null && _assistantCollision == null)
            {
                ChangeButtonState(true, Vector3.zero);
                Stay?.Invoke();
            }

            if (other.TryGetComponent(out AssistantCollision assistantCollision) && _playerCollision == null && _assistantCollision != null)
            {
                ChangeButtonState(true, Vector3.zero);
                Stay?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerCollision player) && _playerCollision != null && _assistantCollision == null)
            {
                ChangeButtonState(false, new Vector3(0, 0.1f, 0));
                _playerCollision = null;
                Exit?.Invoke();
            }

            if (other.TryGetComponent(out AssistantCollision assistant) && _playerCollision == null && _assistantCollision != null)
            {
                ChangeButtonState(false, new Vector3(0, 0.1f, 0));
                _playerCollision = null;
                Exit?.Invoke();
            }
        }

        private void ChangeButtonState(bool value, Vector3 position)
        {
            _button.localPosition = position;
            _isReached = value;
        }
    }
}