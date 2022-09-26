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

    public Cell Cell => _cell;

    public UnityAction PrisonerSendToPool;
    public event UnityAction<PrisonerMover, CellQueueContainer> PrisonerEmptyed; 

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
            if (_prisonerList.Count > 0 && _cellDoor.gameObject.activeInHierarchy && _prisonerList[0].PathEnded())
            {
                if (_isCellBusy && _isCellWorking == false && _prisonerList.Count > 0 && _prisonerList[0].PathEnded())
                {
                    if (_cell.CellDoor.IsOpened)
                        _cell.OnReached();

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
        PrisonerEmptyed?.Invoke(_prisonerList[0], this);
        _prisonerList[0].ChangeStateSitting(false);
        StartCoroutine(CheckCellDoor());
    }

    private void OnPrisonerEnter()
    {
        _isCellBusy = true;
    }

    private IEnumerator CheckCellDoor()
    {
        WaitWhile waitWhile = new WaitWhile(() => _cell.CellDoor.IsOpened == false);

        if (_cell.CheckDoorButton())
            _cell.OnReached();

        yield return waitWhile;

        if (SendToPool(_distributor))
        {
            PrisonerSendToPool?.Invoke();
            _cellDoor.Sale();
        }

        _isCellWorking = false;
        _isCellBusy = false;
    }
}
