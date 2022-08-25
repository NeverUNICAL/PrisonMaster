using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ForCreo
{
    [RequireComponent(typeof(SphereCollider))]
    public class PrisonersQueuePoint : MonoBehaviour
    {
        private SphereCollider _collider;
        private bool _isEmpty = true;

        public bool IsEmpty => _isEmpty;

        public event UnityAction<PrisonerForCreo> TriggerIn;
        public event UnityAction<PrisonerForCreo> TriggerOut;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PrisonerForCreo prisoner))
            {
                _isEmpty = false;
                TriggerIn?.Invoke(prisoner);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PrisonerForCreo prisoner))
            {
                _isEmpty = true;
            }
        }

        public void TakePosition()
        {
            _isEmpty = false;
        }
    }
}