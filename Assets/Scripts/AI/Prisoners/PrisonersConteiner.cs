using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonersConteiner : MonoBehaviour
{
    [SerializeField] private PrisonerMover _prisonerPrefab;
    [SerializeField] private FinishTrigger _finishTrigger;
    [SerializeField] private int _countPrisoners;

    private PrisonerMover[] _prisoners;

    private void Awake()
    {
        _prisoners = new PrisonerMover[_countPrisoners];
        for (int i = 0; i < _prisoners.Length; i++)
        {
            _prisoners[i] = Instantiate(_prisonerPrefab, transform);
            _prisoners[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _finishTrigger.Reached += DeactivatePrisoner;
    }

    private void OnDisable()
    {
        _finishTrigger.Reached -= DeactivatePrisoner;
    }

    public PrisonerMover GetPrisoner()
    {
        for (int i = 0; i < _prisoners.Length; i++)
        {
            if (_prisoners[i].gameObject.activeInHierarchy == false)
            {
                _prisoners[i].gameObject.SetActive(true);
                return _prisoners[i];
            }
        }

        return null;
    }

    private void DeactivatePrisoner(PrisonerMover prisoner)
    {
        prisoner.gameObject.SetActive(false);
        prisoner.transform.position = transform.position;
    }
}
