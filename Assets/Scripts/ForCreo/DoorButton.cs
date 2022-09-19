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
        private BoxCollider _boxCollider;

        public bool IsReached => _isReached;

        public event UnityAction Reached;
        public event UnityAction Exit;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            _button.localPosition = Vector3.zero;
            if (other.TryGetComponent(out PlayerCollision player) && _playerCollision == null)
            {
                _isReached = true;
                _playerCollision = player;
                Reached?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _button.localPosition += new Vector3(0, 0.1f, 0);
            if (other.TryGetComponent(out PlayerCollision player))
            {
                if (_playerCollision == player)
                {
                    _isReached = false;
                    _playerCollision = null;
                    Exit?.Invoke();
                }
            }
        }
    }
}