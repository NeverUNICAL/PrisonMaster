using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agava.IdleGame;
using Agava.IdleGame.Model;
using UnityEngine;
using UnityEngine.Events;

public class CellQueueContainer : QueueHandler
{
    [SerializeField] private Distributor _distributor;
    [SerializeField] private Trigger _trigger;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private float _timeForWashing;
    [SerializeField] private TriggerForPrisoners _triggerForPrisoners;
    [SerializeField] private Cell _cell;
    
    private bool _isCellBusy;
    private bool _isCellWorking;
    private Timer _timer = new Timer();
    
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
        _triggerForPrisoners.Enter += OnPrisonerEnter;
    }

    private void OnDisable()
    {
       // _showerTrigger.Enter -= OnEnter;
      //  _showerTrigger.Exit -= OnExit;
        _timer.Completed -= OnTimeOver;
        _triggerForPrisoners.Enter -= OnPrisonerEnter;
    }
    
    private void Update()
    {
        if (_isCellBusy)
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
                if (_isCellBusy && _isCellWorking == false && _prisonerList.Count > 0 && _prisonerList[0].PathEnded())
                {
                    if (_cell.CellDoor.IsOpened)
                        _cell.OnReached();

                    _cell.AddPrisoner(_prisonerList[0]);
                    _prisonerList[0].ChangeStateSitting(true);
                    _isCellWorking = true;
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
        _prisonerList[0].ChangeStateSitting(false);
        _cell.TryRemovePrisoner(_prisonerList[0]);
        StartCoroutine(CheckCellDoor());
    }

    private void OnPrisonerEnter()
    {
        _isCellBusy = true;
    }

    private IEnumerator CheckCellDoor()
    {
        if (_cell.CheckDoorButton())
            _cell.OnReached();

        yield return new WaitWhile(() => _cell.CellDoor.IsOpened == false);

        if (SendToPool(_distributor))
        {
            //  _store.Sale();
        }

        _isCellWorking = false;
        _isCellBusy = false;
    }
}
