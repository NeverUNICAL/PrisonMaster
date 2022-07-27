using System;
using Agava.IdleGame;
using DG.Tweening;
using System.Collections;
using Agava.IdleGame.Model;
using UnityEngine;

public class Shop : Store
{
    [SerializeField] private StoreCollisionHandler _collisionHandler;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private Transform _point;
    [SerializeField] private int _count;
    [SerializeField] private int _countForSale;
    [SerializeField] float _duration = 1f;
    [SerializeField] private float _durationDelay;

    private float _defaultDuration;

    public int Count => _stackPresenter.Count;
    public int CountForSale => _countForSale;
    
    private void OnEnable()
    {
        _collisionHandler.Triggered += OnBuyerBougth;
    }

    private void OnDisable()
    {
        _collisionHandler.Triggered -= OnBuyerBougth;
    }

    private void Start()
    {
        _defaultDuration = _duration;
    }

    public void Sale()
    {
        for (int i = 0; i < _countForSale; i++)
        {
            StackableObject objectForSale = _stackPresenter.RemoveAt(0);
            objectForSale.View.DOMove(_point.position, _duration).OnComplete(() => Destroy(objectForSale.View.gameObject));
            _duration += _durationDelay;
        }
            
        _duration = _defaultDuration;
        OnSold(_count);
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

