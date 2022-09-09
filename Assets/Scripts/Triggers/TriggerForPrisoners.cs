using UnityEngine;
using UnityEngine.Events;

namespace Agava.IdleGame
{
    [RequireComponent(typeof(Collider))]
    public class TriggerForPrisoners : MonoBehaviour 
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
            if (collider.TryGetComponent(out PrisonerMover prisonerMover))
            {
                Enter?.Invoke();
            }
        }

        private void OnTriggerStay(Collider collider)
        {
            if (collider.TryGetComponent(out PrisonerMover prisonerMover))
            {
                Stay?.Invoke();
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent(out PrisonerMover prisonerMover))
            {
                Exit?.Invoke();
            }
        }
    }
}