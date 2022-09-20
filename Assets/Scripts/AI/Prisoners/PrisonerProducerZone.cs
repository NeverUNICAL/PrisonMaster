using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Agava.IdleGame
{
    public class PrisonerProducerZone : StackInteractableZone
    {
        private PrisonerStackableObjectPresenter _myself;

        public event UnityAction<StackPresenter> Taked;

        private void Awake()
        {
            _myself = GetComponent<PrisonerStackableObjectPresenter>();
            enabled = true;
        }

        protected override bool CanInteract(StackPresenter enteredStack)
        {
            return enteredStack.CanAddToStack(_myself.Layer);
        }

        protected override void InteractAction(StackPresenter enteredStack)
        {
            enteredStack.AddToStackWithoutView(_myself.Stackable);
            Taked?.Invoke(enteredStack);
            enabled = false;
            DropPresenters(enteredStack);
        }
    }
}
