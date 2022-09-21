using ForCreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : Store
{
    [SerializeField] private CellQueueContainer _cellQueue;
    [SerializeField] private int _count;
    [SerializeField] private CellDoor _door;
    [SerializeField] private float _delay = 3f;

    private CellsConteiner _cellsConteiner;

    public List<PrisonerMover> Prisoners => _cellQueue.PrisonerQueueList;
    public CellDoor CellDoor => _door;
    public bool IsPrisonerInCell { get; private set; } = false;

    public event UnityAction PrisonerSendToPool;
    public event UnityAction DoorButtonReached;
    public event UnityAction DoorButtonStay;
    public event UnityAction DoorButtonExit;

    private void Awake()
    {
        _cellsConteiner = GetComponentInParent<CellsConteiner>();
    }

    private void OnEnable()
    {
        _cellQueue.PrisonerSendToPool += OnPrisonerSendToPool;
        _cellsConteiner.DoorButton.Reached += OnReached;
        _cellsConteiner.DoorButton.Stay += OnStay;
        _cellsConteiner.DoorButton.Exit += OnExit;
    }

    private void OnDisable()
    {
        _cellQueue.PrisonerSendToPool -= OnPrisonerSendToPool;
        _cellsConteiner.DoorButton.Reached -= OnReached;
        _cellsConteiner.DoorButton.Stay -= OnStay;
        _cellsConteiner.DoorButton.Exit -= OnExit;
    }

    public void OnReached()
    {
        if (_cellQueue.PrisonerQueueList.Count > 0)
            IsPrisonerInCell = true;

        DoorButtonReached?.Invoke();
    }

    private void OnStay()
    {
        DoorButtonStay?.Invoke();
    }

    private void OnExit()
    {
        DoorButtonExit?.Invoke();
    }

    public override void Sale()
    {
        OnSold(_count);
    }

    private void OnPrisonerSendToPool()
    {
        StartCoroutine(Delay(_delay));
    }

    public bool CheckDoorButton()
    {
        return _cellsConteiner.DoorButton.IsReached;
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        IsPrisonerInCell = false;
        PrisonerSendToPool?.Invoke();
    }
}
