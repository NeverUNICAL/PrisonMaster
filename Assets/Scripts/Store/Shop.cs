using Agava.IdleGame;
using DG.Tweening;
using UnityEngine;

    public class Shop : Store
    {
        [SerializeField] private StoreCollisionHandler _collisionHandler;
        [SerializeField] private StackPresenter _stackPresenter;
        [SerializeField] private Transform _point;

        private void OnEnable()
        {
            _collisionHandler.Triggered += OnBuyerBougth;
        }

        private void OnDisable()
        {
            _collisionHandler.Triggered -= OnBuyerBougth;            
        }

        
        private void Sale()
        {
            if (_stackPresenter.Count > 0)
            {
                var soldObject = _stackPresenter.RemoveAt(_stackPresenter.Count - 1);
                soldObject.View.DOMove(_point.position, 1).OnComplete(() => Destroy(soldObject.View.gameObject));
                OnSold(1);
            }
        }
        
        private void OnBuyerBougth(Buyer buyer)
        {
            Sale();
        }
    }

