using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class StartPool : QueueHandler
{
   // [SerializeField]private PrisonerMover _prisoner;
    [SerializeField] private PrisonersConteiner _prisonersConteiner;
    // [SerializeField]private Transform _spawnPoint;
   // [SerializeField]private Transform _parentPrisoners;

    private PrisonerMover[] _prisoners;
    private bool _isWorking;

    private void Start()
    {
        GenerateList();
        StartCoroutine(Spawn());
        StartCoroutine(SendToQueue(_queues));
    }

    private void ListUpdate()
    {
        _prisonerList.Clear();
        _prisoners = FindObjectsOfType<PrisonerMover>();

        foreach (PrisonerMover prisoner in _prisoners)
        {
            _prisonerList.Add(prisoner);
        }
    }

    public void SetWorking(bool value)
    {
        _isWorking = value;
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return _waitForSpawnTimeOut;

            if (_isWorking)
            {
                if (_prisonerList.Count < _startPoolSize)
                {
                    PrisonerMover prisoner = _prisonersConteiner.GetPrisoner();
                    //_prisonerList.Add(Instantiate(_prisoner, _spawnPoint.position, _spawnPoint.rotation, _parentPrisoners));
                    if (prisoner != null)
                    {
                        _prisonerList.Add(prisoner);
                        ListSort();
                        _prisonerList[0].SetTarget(_firstPoint, Vector3.zero);
                    }
                }
            }
        }
    }
}
