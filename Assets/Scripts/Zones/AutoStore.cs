using System;
using System.Collections;
using Agava.IdleGame;
using DG.Tweening;
using UnityEngine;

    public class AutoStore : Store
    {
        [SerializeField] protected StackPresenter _stack;
        [SerializeField] private Transform _point;
        [SerializeField] private float _delay = 0.5f;

        private bool _isBusy = false;

        private const int DefaultCost = 1;
        private const float Duration = 0.3f;

        public event Action Started;
        public event Action Finished;

        //private void OnEnable()
        //{
        //    _stack.Empty += OnEmpty;
        //    _stack.Filled += OnFilled;
        //}

        //private void OnDisable()
        //{
        //    _stack.Empty -= OnEmpty;
        //    _stack.Filled -= OnFilled;            
        //}

        private void OnFilled()
        {
            if(_isBusy == false)
                StartCoroutine(DelayedSale());
        }

        private void OnEmpty()
        {
            _isBusy = false;
            Finished?.Invoke();
        }

        private void Sale()
        {
            var soldObject = _stack.RemoveAt(_stack.Count - 1);
            soldObject.View.DOMove(_point.position, Duration).OnComplete(() => Destroy(soldObject.View.gameObject));
            OnSold(DefaultCost);
        }

        private IEnumerator DelayedSale()
        {
            var wait = new WaitForSeconds(_delay);

            if(_stack.Count <= 0)
                yield break;

            _isBusy = true;
            Started?.Invoke();

            while(_stack.Count > 0)
            {
                yield return wait;
                Sale();
            }

            _isBusy = false;
        }
    }
