using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonersConteiner : MonoBehaviour
{
    [SerializeField] private FinishTrigger _finishTrigger;

    private PrisonMover[] _prisoners;

    private void Awake()
    {
        _prisoners = new PrisonMover[transform.childCount];
        for (int i = 0; i < _prisoners.Length; i++)
            _prisoners[i] = transform.GetChild(i).GetComponent<PrisonMover>();
    }

    private void OnEnable()
    {
        _finishTrigger.Reached += DeactivatePrisoner;
    }

    private void OnDisable()
    {
        _finishTrigger.Reached -= DeactivatePrisoner;
    }

    public PrisonMover GetPrisoner()
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

    private void DeactivatePrisoner(PrisonMover prison)
    {
        prison.gameObject.SetActive(false);
        prison.transform.position = transform.position;
    }
}
