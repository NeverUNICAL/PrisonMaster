using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Agava.IdleGame.Model;
using System;
using System.Linq;

namespace Agava.IdleGame
{
    public abstract class StackInteractableZone : MonoBehaviour
    {
        [SerializeField] private Trigger<StackPresenter> _trigger;
        [SerializeField] private float _interactionTime = 0.2f;

        private Timer _timer = new Timer();
        //private StackPresenter _enteredStack;
        private StackPresenter[] _enteredStackList;
        private Coroutine _waitCoroutine;

        public ITimer Timer => _timer;
        protected virtual float InteracionTime => _interactionTime;
        protected bool InTrigger = false;

        public event Action Produced;

        private void OnValidate()
        {
            _interactionTime = Mathf.Clamp(_interactionTime, 0.01f, float.MaxValue);
        }

        private void OnEnable()
        {
            _trigger.Enter += OnEnter;
            _trigger.Stay += OnStay;
            _trigger.Exit += OnExit;
            _timer.Completed += OnTimeOver;
        }

        private void OnDisable()
        {
            _trigger.Enter -= OnEnter;
            _trigger.Stay -= OnStay;
            _trigger.Exit -= OnExit;
            _timer.Completed -= OnTimeOver;
        }

        private void Update()
        {
            _timer.Tick(Time.deltaTime);
        }

        public void OnExit(StackPresenter otherStack = null)
        {
            if (_enteredStackList != null && _enteredStackList.Contains(otherStack))
            {
                InTrigger = false;

                if (_waitCoroutine != null)
                    StopCoroutine(_waitCoroutine);

                _timer.Stop();
                Array.Clear(_enteredStackList, 0, _enteredStackList.Length);
                _enteredStackList = null;
            }
        }

        private void OnEnter(StackPresenter enteredStack)
        {
            if (_enteredStackList != null && enteredStack.TryGetComponent(out PlayerMovement player))
            {
                OnExit(_enteredStackList[0]);
                InTrigger = true;
                _enteredStackList = enteredStack.GetComponents<StackPresenter>();

                if (CanInteract(enteredStack))
                    _timer.Start(InteracionTime);
                else
                    _waitCoroutine = StartCoroutine(WaitUntilCanInteract(() => _timer.Start(InteracionTime)));
                
                return;
            }

            if (_enteredStackList != null)
                return;

            InTrigger = true;
            _enteredStackList = enteredStack.GetComponents<StackPresenter>();

            if (CanInteract(enteredStack))
                _timer.Start(InteracionTime);
            else
                _waitCoroutine = StartCoroutine(WaitUntilCanInteract(() => _timer.Start(InteracionTime)));
        }

        private void OnStay(StackPresenter enteredStack)
        {
            if (_enteredStackList == null)
                OnEnter(enteredStack);
        }
        
        private void OnTimeOver()
        {
            foreach (var stackPresenter in _enteredStackList)
                if (CanInteract(stackPresenter))
                    InteractAction(stackPresenter);

            _waitCoroutine = StartCoroutine(WaitUntilCanInteract(() => _timer.Start(InteracionTime)));
        }

        private IEnumerator WaitUntilCanInteract(UnityAction finalAction)
        {
            yield return null;

            yield return new WaitUntil(() =>
            {
                foreach (var stackPresenter in _enteredStackList)
                    if (CanInteract(stackPresenter))
                        return true;

                return false;
            });

            finalAction?.Invoke();
        }

        protected abstract void InteractAction(StackPresenter enteredStack);
        protected abstract bool CanInteract(StackPresenter enteredStack);
    }
}
