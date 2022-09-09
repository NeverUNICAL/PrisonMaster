using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishTrigger : MonoBehaviour
{
    public event UnityAction<PrisonerMover> Reached;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PrisonerMover prisonMover))
        {
            Reached?.Invoke(prisonMover);
        }
    }
}
