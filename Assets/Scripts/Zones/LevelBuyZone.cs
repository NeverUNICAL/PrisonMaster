using System.Configuration;
using UnityEngine;
using Agava.IdleGame.Model;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Agava.IdleGame
{
    public class LevelBuyZone : BuyZonePresenter
    {
        [SerializeField] private NavMeshAgent _targetNavMeshAgent;
        [SerializeField] private int _buyZoneLevelLocated;

        private int _reduceValue = 1;
        
        public event UnityAction<NavMeshAgent,int> LevelUnlocked;

        protected override void BuyFrame(BuyZone buyZone, SoftCurrencyHolder moneyHolder)
        {
            if (moneyHolder.HasMoney == false)
                return;

            _reduceValue = Mathf.Clamp((int)(TotalCost * 1.5f * Time.deltaTime), 1, TotalCost);
            if (buyZone.CurrentCost < _reduceValue)
                _reduceValue = buyZone.CurrentCost;
            

            _reduceValue = Mathf.Clamp(_reduceValue, 1, moneyHolder.Value);

            buyZone.ReduceCost(_reduceValue,TotalCost);
            moneyHolder.Spend(_reduceValue);
        }

        protected override void OnBuyedAction()
        {
            LevelUnlocked?.Invoke(_targetNavMeshAgent,_buyZoneLevelLocated);
        }
    }
}