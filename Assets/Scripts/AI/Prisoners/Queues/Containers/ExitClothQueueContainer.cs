using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agava.IdleGame;
using Agava.IdleGame.Model;
using UnityEngine;
using UnityEngine.Events;
public class ExitClothQueueContainer : QueueHandler
{
    [SerializeField] private Distributor _distributor;
    
    [Header("ShowerSettings")]
    [SerializeField] private Trigger _showerTrigger;
    [SerializeField] private ParticleSystem _showerParticleSystem;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private float _timeForWashing;
    [SerializeField] private Transform _button;
    [SerializeField] private TriggerForPrisoners _triggerForPrisoners;
    
    private bool _onShowerTriggerStayed;
    private bool _isShowerBusy;
    private bool _isShowerWorking;
    private PrisonerMover _prisonerForWash;
    private Timer _timer = new Timer();
    
    public event UnityAction PrisonerIsIn;
    public event UnityAction PrisonerWashEnded;

    private void Start()
    {
        GenerateList();
        StartCoroutine(TryToSendPrisoner());
    }

    private void OnEnable()
    {
        _timerView.Init(_timer);
       // _showerTrigger.Enter += OnEnter;
       // _showerTrigger.Exit += OnExit;
        _timer.Completed += OnTimeOver;
    }

    private void OnDisable()
    {
       // _showerTrigger.Enter -= OnEnter;
      //  _showerTrigger.Exit -= OnExit;
        _timer.Completed -= OnTimeOver;
    }
    
    private void Update()
    {
        if (_isShowerWorking)
        {
            _timer.Tick(Time.deltaTime);
        }
    }
    
    private IEnumerator TryToSendPrisoner()
    {
        while (true)
        {
            if (_prisonerList.Count > 0 && _store.gameObject.activeInHierarchy && _prisonerList[0].PathEnded())
            {
                if (_isShowerWorking == false && _prisonerList.Count > 0 && _prisonerList[0].PathEnded())
                {
                    _isShowerWorking = true;
                    _timer.Start(_timeForWashing);
                }
            }
            
            yield return _waitForSendTimeOut;
        }
    }
    
    /*
    private void OnEnter()
    {
        _onShowerTriggerStayed = true;
        _showerParticleSystem.gameObject.SetActive(true);
        
        if(_isShowerBusy && _isShowerWorking)
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
    */

    private void OnTimeOver()
    {
        if (SendToPool(_distributor))
        {
            _store.Sale();
        }
        
        _isShowerWorking = false;
        PrisonerWashEnded?.Invoke();
        _isShowerBusy = false;
    }
}
