using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAssistant : Assistant
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _delay = 3f;
    [SerializeField] private Transform _arrow;

    private void OnEnable()
    {
        BuyZone.Unlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        BuyZone.Unlocked -= OnUnlocked;
    }
        
    private void OnUnlocked(BuyZonePresenter buyZone)
    {
        if (_arrow != null)
            Destroy(_arrow.gameObject);

        StartCoroutine(DelayMove(_delay));
    }

    private IEnumerator DelayMove(float delay)
    {
        yield return new WaitForSeconds(delay);

        Move(_target.position);
    }
}
