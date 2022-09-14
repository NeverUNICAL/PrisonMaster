using Agava.IdleGame;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    [SerializeField] private BuyZonePresenter _buyZone;
    [SerializeField] private float _scaleDuration = 0.5f;

    private void OnEnable()
    {
        _buyZone.Unlocked += DeactivatePlug;
    }

    private void OnDisable()
    {
        _buyZone.Unlocked -= DeactivatePlug;
    }

    private void DeactivatePlug(BuyZonePresenter buyZone)
    {
        transform.DOScale(Vector3.zero, _scaleDuration).OnComplete(() => gameObject.SetActive(false));
    }
}
