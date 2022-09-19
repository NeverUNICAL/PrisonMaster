using UnityEngine;
using Agava.IdleGame.Model;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Agava.IdleGame
{
    public class RoomBuyZone : BuyZonePresenter
    {
        [SerializeField] private OfficeDoor _door;
        [SerializeField] private GameObject[] _locks;
        
        private int _reduceValue = 1;
        
        public event UnityAction<int,int> LevelUnlocked;

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
            _door.enabled = true;
            for (int i = 0; i < _locks.Length; i++)
            {
             _locks[i].SetActive(false);
            }
        }
    }
}