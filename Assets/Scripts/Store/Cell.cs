using ForCreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : Store
{
    [SerializeField] private int _count;
    [SerializeField] private CellDoor _door;

    private CellsConteiner _cellsConteiner;
    private List<PrisonerMover> _prisoners = new List<PrisonerMover>();

    public List<PrisonerMover> Prisoners => _prisoners;
    public CellDoor CellDoor => _door;

    public event UnityAction DoorButtonReached;
    public event UnityAction DoorButtonExit;

    private void Awake()
    {
        _cellsConteiner = GetComponentInParent<CellsConteiner>();
    }

    private void OnEnable()
    {
        _cellsConteiner.DoorButton.Reached += OnReached;
        _cellsConteiner.DoorButton.Exit += OnExit;
    }

    private void OnDisable()
    {
        _cellsConteiner.DoorButton.Reached -= OnReached;
        _cellsConteiner.DoorButton.Exit -= OnExit;
    }

    public void OnReached()
    {
        DoorButtonReached?.Invoke();
    }

    private void OnExit()
    {
        DoorButtonExit?.Invoke();
    }

    public override void Sale()
    {
        OnSold(_count);
    }

    public void AddPrisoner(PrisonerMover prisonerMover)
    {
        _prisoners.Add(prisonerMover);
    }

    public void TryRemovePrisoner(PrisonerMover prisonerMover)
    {
        StartCoroutine(CheckCellDoor(prisonerMover));
    }

    public bool CheckDoorButton()
    {
        return _cellsConteiner.DoorButton.IsReached;
    }

    private IEnumerator CheckCellDoor(PrisonerMover prisonerMover)
    {
        yield return new WaitWhile(() => _door.IsOpened == false);

        _prisoners.Remove(prisonerMover);
    }
}
