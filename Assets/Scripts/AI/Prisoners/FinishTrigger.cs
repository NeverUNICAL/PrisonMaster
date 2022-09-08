using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishTrigger : MonoBehaviour
{
    public event UnityAction<PrisonMover> Reached;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PrisonMover prisonMover))
        {
            Reached?.Invoke(prisonMover);
        }
    }
}
