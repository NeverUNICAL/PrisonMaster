using System;
using UnityEngine;
using UnityEngine.Events;

namespace Agava.IdleGame
{
    [RequireComponent(typeof(Collider))]
    public class PlayerCollision : MonoBehaviour
    {
        private SoftCurrencyHolder _playerBalance;

        public bool OnTrigger { get; private set; } = false;

        public event Action OnDigArea;
        public event Action OnWorkArea;
        public event Action MoneyCollected;

        private void Awake()
        {
            _playerBalance = GetComponent<SoftCurrencyHolder>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MoneyConteiner moneyConteiner))
            {
                moneyConteiner.ChangeWorking(true);
                OnMoneyCollided(moneyConteiner);
            }

            if (other.TryGetComponent(out MineTrigger _))
            {
                OnTrigger = true;
                OnDigArea?.Invoke();
            }

            if (other.TryGetComponent(out ManualMachineTrigger _))
            {
                OnTrigger = true;
                OnWorkArea?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MoneyConteiner moneyConteiner))
                moneyConteiner.ChangeWorking(false);
            

            if (other.TryGetComponent(out Trigger _))
                OnTrigger = false;
        }

        //private void OnMoneyCollided(Money money)
        //{
        //    money.OnCollected(transform);
        //    SoftCurrencyHolder playerBalance = GetComponent<SoftCurrencyHolder>();
        //    playerBalance.Add(money.Reward);
        //    MoneyCollected?.Invoke();
        //}

        private void OnMoneyCollided(MoneyConteiner moneyConteiner)
        {
            moneyConteiner.GetMoney(_playerBalance);
            MoneyCollected?.Invoke();
        }
    }
}
