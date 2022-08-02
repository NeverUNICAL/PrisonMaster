using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialMoney : MonoBehaviour
{
    private float _checkCooldawn = 0.5f;
    private Coroutine _coroutineInJob;

    public event UnityAction MoneyPullEmpty;

    private void Start()
    {
        _coroutineInJob = StartCoroutine(CheckMoneyCount());
    }

    private IEnumerator CheckMoneyCount()
    {
        while(true)
        {
        yield return new WaitForSeconds(_checkCooldawn);

            if (transform.childCount == 0)
            {
                MoneyPullEmpty?.Invoke();
                StopCoroutine(_coroutineInJob);
            }

        }
    }
}
