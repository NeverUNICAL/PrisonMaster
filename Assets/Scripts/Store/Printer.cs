using System;
using Agava.IdleGame;
using DG.Tweening;
using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField] private ItemCreator _itemCreator;
    [SerializeField] private MeshRenderer _meshRenderer;

    private void OnEnable()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.7f);
    }

    private void Update()
    {
        if(_meshRenderer != null && _itemCreator.IStackIsFull != true)
            _meshRenderer.materials[3].mainTextureOffset += new Vector2(0, 0.005f);
    }
}
