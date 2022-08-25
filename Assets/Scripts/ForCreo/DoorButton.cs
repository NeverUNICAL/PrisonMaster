using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ForCreo
{
    [RequireComponent(typeof(BoxCollider))]
    public class DoorButton : MonoBehaviour
    {
        private BoxCollider _boxCollider;

        public event UnityAction Reached;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement player))
                Reached?.Invoke();
        }
    }
}