using UnityEngine;
using Agava.IdleGame.Model;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Agava.IdleGame
{
    public class RoomBuyZone : BuyZonePresenter
    {
        [SerializeField] private Door _door;
        [SerializeField] private GameObject _lock;
        
        private int _reduceValue = 1;
        
        public event UnityAction<int,int> LevelUnlocked;

        protected override void BuyFrame(BuyZone buyZone, SoftCurrencyHolder moneyHolder)
        {
            if (moneyHolder.HasMoney == false)
                return;

            _reduceValue = Mathf.Clamp((int)(TotalCost * 1.5f * Time.deltaTime), 1, TotalCost);
            if (buyZone.CurrentCost < _reduceValue)
                _reduceValue = buyZone.CurrentCost;
            

            _reduceValue = Mathf.Clamp(_reduceValue, 1, moneyHolder.Value);

            buyZone.ReduceCost(_reduceValue,TotalCost);
            MoneyShooter shooter = FindObjectOfType<MoneyShooter>();
            shooter.Shoot(transform);
            moneyHolder.Spend(_reduceValue);
        }

        protected override void OnBuyedAction()
        {
            _door.enabled = true;
            _lock.SetActive(false);
        }
    }
}