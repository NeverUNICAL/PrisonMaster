using System;
using UnityEngine;

namespace Agava.IdleGame
{
    public class ObjectTransferZone : StackInteractableZone
    {
        [SerializeField] private StackPresenter _selfStack;

        public event Action Transfered;

        protected override bool CanInteract(StackPresenter enteredStack)
        {
            if (_selfStack.Count == 0)
                return false;

            foreach (var item in _selfStack.Data)
                if (enteredStack.CanAddToStack(item.Layer))
                    return true;

            return false;
        }

        protected override void InteractAction(StackPresenter enteredStack)
        {
            int index = 0;

            foreach (var item in _selfStack.Data)
            {
                if (enteredStack.CanAddToStack(item.Layer))
                    break;

                index++;
            }

            if (index >= _selfStack.Count)
                throw new InvalidOperationException();

            var stackable = _selfStack.RemoveAt(index);
            enteredStack.AddToStack(stackable);
            Transfered?.Invoke();
        }
    }
}