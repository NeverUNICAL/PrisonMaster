using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFinishTrigger : MonoBehaviour
{
    private bool _isFinish = false;

    public event UnityAction FinishReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerAnimator playerAnimator))
        {
            if (_isFinish)
                FinishReached?.Invoke();
        }    
    }

    public void ChangeFinishState()
    {
        _isFinish = true;
    }
}
