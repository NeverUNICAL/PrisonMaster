using UnityEngine;
using Agava.IdleGame.Model;
using UnityEngine.Events;

namespace Agava.IdleGame
{
    public class AssistantBuyZone : BuyZonePresenter
    {
        [SerializeField] private Transform _outline;
        [SerializeField] private int _number;
        [SerializeField] private AssistantsSavePresenter _assistantsSavePresenter;

        private int _reduceValue = 1;

        public Transform Outline => _outline;
        
        protected override void BuyFrame(BuyZone buyZone, SoftCurrencyHolder moneyHolder)
        {
            if (moneyHolder.HasMoney == false)
            {
                moneyHolder.SetVFXEnabled(false);
                return;
            }
            
            moneyHolder.SetVFXEnabled(true);
            _reduceValue = Mathf.Clamp((int)(TotalCost * 1.5f * Time.deltaTime), 1, TotalCost);
            if (buyZone.CurrentCost < _reduceValue)
                _reduceValue = buyZone.CurrentCost;
            
            _reduceValue = Mathf.Clamp(_reduceValue, 1, moneyHolder.Value);

            buyZone.ReduceCost(_reduceValue,TotalCost);
            moneyHolder.Spend(_reduceValue);
        }

        protected override void OnBuyedAction()
        {
            _assistantsSavePresenter.ChangeCount(_number);
            transform.gameObject.SetActive(false);
        }
    }
}