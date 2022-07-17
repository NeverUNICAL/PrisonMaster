using System;
using Agava.IdleGame;
using Agava.IdleGame.Model;
using DG.Tweening;
using UnityEngine;

public class Printer : Construction
{
    [SerializeField] private ItemCreator _itemCreator;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _conveerSpeed = 0.005f;

    private Vector2 _vector2Speed;

    private void Start()
    {
        _vector2Speed = new Vector2(0, _conveerSpeed);
    }

    private void Update()
    {
        if (_meshRenderer != null && _itemCreator.IStackIsFull != true)
            _meshRenderer.materials[3].mainTextureOffset += _vector2Speed;
    }
}
