using Agava.IdleGame;
using Agava.IdleGame.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrisonerStackableObjectPresenter : MonoBehaviour
{
    [SerializeField]
    [StackableLayer] private int _layer;
    [SerializeField] private PrisonerType _prisonerType;

    private StackableObject _stackable;

    public StackableObject Stackable => _stackable;
    public int Layer => _layer;
    //public event UnityAction<Transform> PositionChanding;
    //public event UnityAction Dropped;
    //public event UnityAction Eated;
    //public event UnityAction Vaccinated;
    //public event UnityAction<AnimalNeeds> NeedDefined;
    //public event UnityAction<AnimalNeeds> NeedSatisfied;

    private void Awake()
    {
        _stackable = new StackableObject(transform, _layer);
    }

    //private void OnEnable()
    //{
    //    _stackable.LayerChanged += OnLayerChanged;
    //    _stackable.GoingToPosition += OnGoingToPosition;
    //    _stackable.Reseted += OnReseted;
    //    _stackable.Eated += OnEating;
    //    _stackable.Vaccinated += OnVaccinated;
    //    _stackable.NeedDefined += OnNeedDefined;
    //    _stackable.NeedSatisfied += OnNeedSatisfied;
    //}

    //private void OnDisable()
    //{
    //    _stackable.LayerChanged -= OnLayerChanged;
    //    _stackable.GoingToPosition -= OnGoingToPosition;
    //    _stackable.Reseted -= OnReseted;
    //    _stackable.Eated -= OnEating;
    //    _stackable.Vaccinated -= OnVaccinated;
    //    _stackable.NeedDefined -= OnNeedDefined;
    //    _stackable.NeedSatisfied -= OnNeedSatisfied;
    //}

    private void OnLayerChanged(int layer)
    {
        _layer = layer;
    }

    //private void OnGoingToPosition(Transform position)
    //{
    //    PositionChanding?.Invoke(position);
    //}

    //private void OnReseted()
    //{
    //    Dropped?.Invoke();
    //}

    //private void OnEating()
    //{
    //    Eated?.Invoke();
    //}

    //private void OnVaccinated()
    //{
    //    Vaccinated.Invoke();
    //}

    //private void OnNeedDefined(AnimalNeeds animalNeed)
    //{
    //    NeedDefined?.Invoke(animalNeed);
    //}

    //private void OnNeedSatisfied(AnimalNeeds animalNeed)
    //{
    //    NeedSatisfied?.Invoke(animalNeed);
    //}
}

public enum PrisonerType
{
    Default,
    Inflated
}