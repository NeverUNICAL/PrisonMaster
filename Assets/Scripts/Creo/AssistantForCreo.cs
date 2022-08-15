using System;
using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public class AssistantForCreo : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _imageCard;
    [SerializeField] private AssistantTriggerCreo _collider;
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxStackView _stack;
    [SerializeField] private ParticleSystem _angry;
    [SerializeField] private float _delay = 2f;

    private void OnEnable()
    {
        _stack.SuitReached += OnReached;
        _collider.Enter += OnEnter;
        _collider.Exit += OnExit;
    }

    private void OnDisable()
    {
        _stack.SuitReached -= OnReached;
        _collider.Enter -= OnEnter;
        _collider.Exit -= OnExit;
    }

    private void OnEnter()
    {
        _animator.SetTrigger("Ask");
        _image.DOFade(1, 0.5f);
        _imageCard.DOFade(1, 0.5f);
        _collider.ChangeState(false);
    }

    private void OnExit()
    {
        _image.DOFade(0, 0.5f);
        _imageCard.DOFade(0, 0.5f);
    }

    private void OnReached()
    {
        _animator.SetTrigger("Angry");
        if (_angry.isPlaying == false)
            _angry.Play();

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        OnEnter();

        yield return new WaitForSeconds(_delay);

        OnExit();
    }
}
