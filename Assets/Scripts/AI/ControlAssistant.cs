using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAssistant : Assistant
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _delay = 3f;
    [SerializeField] private AssistantBuyZone _buyZone;

    public AssistantBuyZone BuyZone => _buyZone;

    private void OnEnable()
    {
        _buyZone.Unlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        _buyZone.Unlocked -= OnUnlocked;
    }
        
    private void OnUnlocked(BuyZonePresenter buyZone)
    {
        StartCoroutine(DelayMove(_delay));
    }

    private IEnumerator DelayMove(float delay)
    {
        yield return new WaitForSeconds(delay);

        Move(_target.position);
    }
}
