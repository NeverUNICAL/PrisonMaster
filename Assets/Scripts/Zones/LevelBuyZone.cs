using UnityEngine;
using Agava.IdleGame.Model;
using UnityEngine.Events;

namespace Agava.IdleGame
{
    public class LevelBuyZone : BuyZonePresenter
    {
        [SerializeField] private int _navMeshMaskId;
        [SerializeField] private int _buyZoneLevelLocated;
        [SerializeField] private MeshRenderer[] _meshRendererUnlockables;
        [SerializeField] private Material _unlockedMaterial;
        [SerializeField] private GameObject _objectToTurnOff;
        [SerializeField] private PlayerSavePresenter _playerSavePresenter;

        [SerializeField] private Transform _outline;

        public Transform Outline => _outline;

        private int _reduceValue = 1;
        
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
            _playerSavePresenter.OnLevelUnlocked(_navMeshMaskId,_buyZoneLevelLocated);
            
            foreach (MeshRenderer meshRenderer in _meshRendererUnlockables)
            {
                meshRenderer.material = _unlockedMaterial;
            }
            
            _objectToTurnOff.SetActive(false);
            transform.gameObject.SetActive(false);
        }
    }
}