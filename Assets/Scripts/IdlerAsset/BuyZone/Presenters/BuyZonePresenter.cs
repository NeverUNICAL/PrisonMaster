using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Agava.IdleGame.Model;
using UnityEngine.UI;

namespace Agava.IdleGame
{
    public abstract class BuyZonePresenter : GUIDMonoBehaviour
    {
        [Space(10)]
        [SerializeField] private int _totalCost;
        [SerializeField] private Trigger<SoftCurrencyHolder> _trigger;
        [SerializeField] private BuyZoneView _view;
        [SerializeField] private UnlockableObject _unlockable;
        [SerializeField] private Image _image;

        private BuyZone _buyZone;
        private Coroutine _tryBuy;
        private WaitForSeconds _delayForTryBuy = new WaitForSeconds(0.45f);
        private bool _onZone;

        public event UnityAction<BuyZonePresenter> FirstTimeUnlocked;
        public event UnityAction<BuyZonePresenter> Unlocked;

        public int TotalCost => _totalCost;
        public UnlockableObject UnlockableObject => _unlockable;

        private void OnValidate()
        {
            _view?.RenderCost(_totalCost);
        }

        private void Awake()
        {
            _buyZone = new BuyZone(_totalCost, GUID);
        }

        private void OnEnable()
        {
            _trigger.Enter += OnPlayerTriggerEnter;
            _trigger.Exit += OnPlayerTriggerExit;
            _buyZone.Unlocked += OnBuyZoneUnlocked;
            _buyZone.CostUpdated += UpdateCost;

            OnEnabled();
        }

        private void OnDisable()
        {
            _trigger.Enter -= OnPlayerTriggerEnter;
            _trigger.Exit -= OnPlayerTriggerExit;
            _buyZone.Unlocked -= OnBuyZoneUnlocked;
            _buyZone.CostUpdated -= UpdateCost;

            OnDisabled();
        }

        private void Start()
        {
            _buyZone.Load();
            UpdateCost();

            OnBuyZoneLoaded(_buyZone);
        }

        public void Init(int totalCost)
        {
            _totalCost = totalCost;
            _buyZone = new BuyZone(_totalCost, GUID);
            Start();
        }

        private void OnPlayerTriggerEnter(SoftCurrencyHolder moneyHolder)
        {
            _onZone = true;
            
            _tryBuy = StartCoroutine(DelayBeforeTryBuy(moneyHolder));
           
        }

        private void OnPlayerTriggerExit(SoftCurrencyHolder moneyHolder)
        {
            _onZone = false;
            
            StopCoroutine(_tryBuy);
            _buyZone.Save();
            OnExit();
            moneyHolder.SetVFXEnabled(false);
        }

        private void OnBuyZoneUnlocked(bool onLoad)
        {
            _trigger.Disable();
            _view.Hide();
            _unlockable.Unlock(transform, onLoad, GUID);
            OnBuyedAction();

            Unlocked?.Invoke(this);
        }
        
        private IEnumerator DelayBeforeTryBuy(SoftCurrencyHolder moneyHolder)
        {
            yield return _delayForTryBuy;
            
            if (_tryBuy != null)
                StopCoroutine(_tryBuy);

            if (_onZone)
            {
                _tryBuy = StartCoroutine(TryBuy(moneyHolder));
                OnEnter();
            }
        }

        private IEnumerator TryBuy(SoftCurrencyHolder moneyHolder)
        {
            yield return null;

            while (true)
            {
                BuyFrame(_buyZone, moneyHolder);
                UpdateCost();

                yield return null;
            }
        }

        private void UpdateCost()
        {
            _view.RenderCost(_buyZone.CurrentCost);
            if (_image != null)
                _image.fillAmount = _buyZone.CurrentPercent;
        }

        public void Unlock()
        {
            _view.Hide();
            _unlockable.Unlock(transform, false, GUID);
            OnBuyedAction();

            _trigger.gameObject.SetActive(false);
            Unlocked?.Invoke(this);
        }

        protected virtual void OnBuyZoneLoaded(BuyZone buyZone) { }
        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected abstract void BuyFrame(BuyZone buyZone, SoftCurrencyHolder moneyHolder);
        protected virtual void OnBuyedAction() { }
    }
}