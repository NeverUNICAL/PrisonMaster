using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Agava.IdleGame;

public class AssistantTriggerCreo : MonoBehaviour
{
    public event UnityAction Enter;
    public event UnityAction Exit;

    private bool _isFirst = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStackPresenter player))
        {
            if (_isFirst)
            {
                Enter?.Invoke();
                _isFirst = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerStackPresenter player))
        {

            Exit?.Invoke();
        }
    }

    public void ChangeState(bool value)
    {
        _isFirst = value;
    }
}
