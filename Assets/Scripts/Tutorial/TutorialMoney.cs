using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialMoney : MonoBehaviour
{
    private float _checkCooldown = 0.5f;
    private Coroutine _coroutineInJob;
    private WaitForSeconds _waitForCheckCooldown;

    public event UnityAction MoneyPullEmpty;

    private void Awake()
    {
        _waitForCheckCooldown = new WaitForSeconds(_checkCooldown);
    }

    private void Start()
    {
        _coroutineInJob = StartCoroutine(CheckMoneyCount());
    }

    private IEnumerator CheckMoneyCount()
    {
        while(true)
        {
         yield return _waitForCheckCooldown;

            if (transform.childCount == 0)
            {
                MoneyPullEmpty?.Invoke();
                StopCoroutine(_coroutineInJob);
            }
        }
    }
}
