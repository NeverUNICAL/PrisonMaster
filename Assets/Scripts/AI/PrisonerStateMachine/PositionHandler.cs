using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class PositionHandler : MonoBehaviour
{
    public bool IsEmpty { get; private set; } = true;
    public bool IsPrisonerReached { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Prisoner prisoner))
        {
            IsPrisonerReached = true;
            IsEmpty = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Prisoner prisoner))
        {
            IsPrisonerReached = false;
            IsEmpty = true;
        }
    }

    public void SetEmpty(bool value)
    {
        IsEmpty = value;
    }
}
