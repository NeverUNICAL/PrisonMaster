using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStateChanger : MonoBehaviour
{
    [SerializeField] private Transform _arrow;

    private Trigger _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Trigger>();
    }

    private void OnEnable()
    {
        _trigger.Enter += OnEnter;
    }

    private void OnEnter()
    {
        _arrow.gameObject.SetActive(false);
        _trigger.Enter -= OnEnter;
    }
}
