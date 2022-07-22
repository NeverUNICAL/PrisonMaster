using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Transform[] _openObjects;
    [SerializeField] private NormalBuyZonePresenter _producerZone;
    [SerializeField] private NormalBuyZonePresenter _consumerZone;
    [SerializeField] private Transform[] _arrows;
    [SerializeField] private Transform[] _outlines;
    [SerializeField] private float _duration;
    [SerializeField] private float _durationForOutlines;
    [SerializeField] private Vector3 _scaleTarget;

    private void OnEnable()
    {
        _producerZone.Unlocked += OnUnlock;
        _consumerZone.Unlocked += OnStartGame;
    }

    private void OnDisable()
    {
        _producerZone.Unlocked -= OnUnlock; 
        _consumerZone.Unlocked += OnStartGame;
    }

    private void Start()
    {
        _arrows[0].transform.DOMove(new Vector3(_arrows[0].transform.position.x, _arrows[0].transform.position.y - 0.5f, _arrows[0].transform.position.z), _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _outlines[0].transform.DOScale(_scaleTarget, _durationForOutlines).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnUnlock(BuyZonePresenter normalBuyZonePresenter)
    {
        _consumerZone.gameObject.SetActive(true);
        _outlines[1].transform.DOScale(_scaleTarget, _durationForOutlines).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _arrows[0].gameObject.SetActive(false);
        _arrows[1].gameObject.SetActive(true);
        _arrows[1].transform.DOMove(new Vector3(_arrows[1].transform.position.x, _arrows[1].transform.position.y - 0.5f, _arrows[1].transform.position.z), _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnStartGame(BuyZonePresenter normalBuyZonePresenter)
    {
        for (int i = 0; i < _openObjects.Length; i++)
        {
            _openObjects[i].gameObject.SetActive(true);
        }

        this.gameObject.SetActive(false);
    }
}
