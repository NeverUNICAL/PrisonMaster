using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PrisonerSpawner : MonoBehaviour
{
    public bool IsEmpty { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Prisoner prisoner))
            IsEmpty = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Prisoner prisoner))
            IsEmpty = true;
    }
}
