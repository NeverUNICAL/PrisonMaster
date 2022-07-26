using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishTrigger : MonoBehaviour
{
    public event UnityAction Reached;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PrisonMover prisonMover))
        {
            Reached?.Invoke();
            Destroy(prisonMover.gameObject);
        }
    }
}
