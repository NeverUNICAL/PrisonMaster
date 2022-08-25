using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class StartPool : QueueHandler
{
    [SerializeField] private PrisonMover _prisoner;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _parentPrisoners;

    private PrisonMover[] _prisoners;
    private bool _isWorking;

    private void Start()
    {
        GenerateList();
        StartCoroutine(Spawn());
        StartCoroutine(SendToQueue(_queues));
    }

    public void ChangeSpawningState(bool value)
    {
        _isWorking = value;
        if (value)
            StartCoroutine(Spawn());
    }

    private void ListUpdate()
    {
        _prisonerList.Clear();
        _prisoners = FindObjectsOfType<PrisonMover>();

        foreach (PrisonMover prisoner in _prisoners)
        {
            _prisonerList.Add(prisoner);
        }
    }

    private IEnumerator Spawn()
    {
        while (_isWorking)
        {
            yield return _waitForSpawnTimeOut;

            if (_prisonerList.Count < _startPoolSize)
            {
                _prisonerList.Add(Instantiate(_prisoner, _spawnPoint.position, _spawnPoint.rotation, _parentPrisoners));
                ListSort();
                _prisonerList[0].SetTarget(_firstPoint, Vector3.zero);
            }
        }
    }
}
