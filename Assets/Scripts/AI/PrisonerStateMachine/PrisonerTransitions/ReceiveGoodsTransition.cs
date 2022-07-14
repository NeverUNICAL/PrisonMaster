using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveGoodsTransition : PrisonerTransition
{
    [SerializeField] private ObjectTransferZone[] _transferZones;
    [SerializeField] private float _delay;

    private void OnEnable()
    {
        for (int i = 0; i < _transferZones.Length; i++)
        {
            _transferZones[i].ClearenceComplete += CheckNextTransit;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _transferZones.Length; i++)
        {
            _transferZones[i].ClearenceComplete -= CheckNextTransit;
        }
    }

    public void CheckNextTransit(ObjectTransferZone transferZone)
    {
        
            StartCoroutine(DelayCheck(_delay));
    }

    private IEnumerator DelayCheck(float delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < TargetPositions.Length; i++)
        {
            if (TargetPositions[i].IsEmpty == true)
                NeedTransit = true;
        }
            NeedAlternativeTransit = true;
    }
}
