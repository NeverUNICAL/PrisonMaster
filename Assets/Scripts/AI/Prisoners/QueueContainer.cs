using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agava.IdleGame;
using Agava.IdleGame.Model;
using UnityEngine;

public class QueueContainer : QueueHandler
{
    [SerializeField] private Distributor _distributor;
    
    [Header("ShowerSettings")]
    [SerializeField] private bool _isShower;
    [SerializeField] private bool _isBeforeShower;
    [SerializeField] private Trigger _showerTrigger;
    [SerializeField] private ParticleSystem _showerParticleSystem;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private float _timeForWashing;
    [SerializeField] private List<QueueContainer> _listForShower;
    [SerializeField] private Transform _button;
    [SerializeField] private Animator _inCoverAnimator;
    [SerializeField] private Animator _outCoverAnimator;
    [SerializeField] private float _delayCloseInCover;
    [SerializeField] private float _delayCloseOutCover;

    private bool _onShowerTriggerStayed;
    private bool _isShowerBusy;
    private PrisonMover _prisonerForWash;
    private Timer _timer = new Timer();
    private bool _isAngryPrisoner;
    
    private void Start()
    {
        GenerateList();
        StartCoroutine(TryToSendPrisoner());
    }

    private void OnEnable()
    {
        if (_isShower)
        {
            _listForShower = new List<QueueContainer>();
            _inCoverAnimator.SetBool("IsOpened",true);
            _timerView.Init(_timer);
            _showerTrigger.Enter += OnEnter;
            _showerTrigger.Exit += OnExit;
            _timer.Completed += OnTimeOver;
        }
    }

    private void OnDisable()
    {
        if (_isShower)
        {
            _showerTrigger.Enter -= OnEnter;
            _showerTrigger.Exit -= OnExit;
            _timer.Completed -= OnTimeOver;
        }
    }

    private void Update()
    {
        if (_onShowerTriggerStayed)
        {
            _timer.Tick(Time.deltaTime);
        }

        if (_isBeforeShower && _listForShower[0]._prisonerList.Count > 0 && _listForShower[0]._prisonerList[0].PathEnded())
        {
            if(_inCoverAnimator.GetBool("IsOpened"))
             Invoke(nameof(CloseInCover), _delayCloseInCover);
        }
    }

    public bool CheckForShopBuyed()
    {
        if (_shop.gameObject.activeInHierarchy)
            return true;

        return false;
    }

    private IEnumerator TryToSendPrisoner()
    {
        while (true)
        {
            if (_prisonerList.Count > 0 && _shop.gameObject.activeInHierarchy && _prisonerList[0].PathEnded())
            {
                if (_shop.Count >= _shop.CountForSale)
                {
                    if (_isShower && _onShowerTriggerStayed && _isShowerBusy == false && _prisonerList.Count > 0 && _prisonerList[0].PathEnded()
                        && _inCoverAnimator.GetBool("IsOpened") == false)
                    {
                       _timer.Start(_timeForWashing);
                       _isShowerBusy = true;
                    }
                    else if(_isShower == false && _isBeforeShower == false)
                    {
                        if (SendToPool(_distributor))
                            _shop.Sale();
                    }
                    else if(_isBeforeShower)
                    {
                        SendToShower();
                    }
                    
                    ChangeAnimationPrisoners(false);
                }
                else
                {
                    ChangeAnimationPrisoners(true);
                }
            }

            yield return _waitForSendTimeOut;
        }
    }

    private void SendToShower()
    {
        if (_listForShower[0].PrisonerQueueList.Count < _listForShower[0].PoolSize)
        {
            if (_prisonerList.Count > 0)
            {
                _listForShower[0].PrisonerQueueList.Add(_prisonerList[0]);
                _listForShower[0].ListSort();
                ExtractFirst();
            }
        }
    }

    private void ChangeAnimationPrisoners(bool value)
    {
        foreach (var prisoner in _prisonerList)
        {
            if (prisoner.PathEnded())
                prisoner.ChangeAngryAnimation(value);
        }
    }

    private void OnEnter()
    {
        _onShowerTriggerStayed = true;
        _showerParticleSystem.gameObject.SetActive(true);
        
        if(_isShowerBusy)
         _timer.Start(_timeForWashing);
        
        _button.localPosition = Vector3.zero;
    }

    private void OnExit()
    {
        _timer.Stop();
        _onShowerTriggerStayed = false;
        _showerParticleSystem.gameObject.SetActive(false);
       
        _button.localPosition += new Vector3(0, 0.1f, 0);
    }

    private void OnTimeOver()
    {
        _outCoverAnimator.SetBool("IsOpened",true);
        _inCoverAnimator.SetBool("IsOpened",true);
        _prisonerList[0].EnableSuitAfterShower();
        
        if (SendToPool(_distributor))
            _shop.Sale();
        
        _isShowerBusy = false;
        Invoke(nameof(CloseOutCover), _delayCloseOutCover);
    }

    private void CloseOutCover()
    {
        _outCoverAnimator.SetBool("IsOpened",false);
    }
    
    private void CloseInCover()
    {
        _inCoverAnimator.SetBool("IsOpened",false);
    }
}
