using Agava.IdleGame;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Shop : Store
{
    [SerializeField] private StoreCollisionHandler _collisionHandler;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private Transform _point;

    public int Count => _stackPresenter.Count;

    private float _duration = 1f;
    private void OnEnable()
    {
        _collisionHandler.Triggered += OnBuyerBougth;
    }

    private void OnDisable()
    {
        _collisionHandler.Triggered -= OnBuyerBougth;
    }
    
    public void Sale()
    {
        if (_stackPresenter.Count > 2)
        {
            for (int i = 0; i < _stackPresenter.Count; i++)
            {
                if (i < 3)
                {
                    var soldObject = _stackPresenter.RemoveAt(_stackPresenter.Count - 1);
                    soldObject.View.DOMove(_point.position, _duration).OnComplete(() => Destroy(soldObject.View.gameObject));
                    _duration ++;
                }
            }
            
            _duration = 1f;
            OnSold(3);
        }
    }

    private void OnBuyerBougth(Buyer buyer)
    {
        Sale();
    }

    public StackPresenter GetStackPresenter()
    {
        return _stackPresenter;
    }
}

