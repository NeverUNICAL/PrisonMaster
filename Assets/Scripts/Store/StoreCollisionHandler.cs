using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StoreCollisionHandler : MonoBehaviour
{
    public event Action<Buyer> Triggered;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent(out Buyer buyer))
    //        OnTriggered(buyer);
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Buyer buyer))
            OnTriggered(buyer);
    }

    private void OnTriggered(Buyer buyer)
    {
        Triggered?.Invoke(buyer);
    }
}

