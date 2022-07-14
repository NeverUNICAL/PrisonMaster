using System;
using Agava.IdleGame.Model;
using UnityEngine;
using UnityEngine.Events;

namespace Agava.IdleGame
{
    public class ObjectTransferZone : StackInteractableZone
    {
        [SerializeField] private StackPresenter _selfStack;
        [SerializeField] private PositionHandler _positionHandler;

        public event Action Transfered;
        public event UnityAction<ObjectTransferZone> ClearenceComplete;

        public int Count => _selfStack.Count;

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

            StackableObject stackable = _selfStack.RemoveAt(index);
            enteredStack.AddToStack(stackable);
            Transfered?.Invoke();
        }

        private void Update()
        {
            if (_positionHandler.IsPrisonerReached && _selfStack.Count != 0)
                ClearenceComplete?.Invoke(this);
        }
    }
}