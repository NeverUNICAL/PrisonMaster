using Agava.IdleGame.Model;
using UnityEngine;

namespace Agava.IdleGame
{
    public class SoftCurrencyHolder : CurrencyHolder
    {
        [SerializeField] private ParticleSystem _moneyVFX;
        
        protected override Currency InitCurrency() => new SoftCurrency();

        public void SetVFXEnabled(bool value)
        {
         _moneyVFX.gameObject.SetActive(value);   
        }
    }
}