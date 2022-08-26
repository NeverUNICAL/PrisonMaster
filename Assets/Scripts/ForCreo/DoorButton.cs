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

        private BoxCollider _boxCollider;

        public event UnityAction Reached;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            _button.localPosition = Vector3.zero;
            if (other.TryGetComponent(out PlayerMovement player))
                Reached?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            _button.localPosition += new Vector3(0, 0.1f, 0);
        }
    }
}