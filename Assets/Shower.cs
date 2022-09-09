using System;
using Agava.IdleGame;
using DG.Tweening;
using System.Collections;
using Agava.IdleGame.Model;
using UnityEngine;

public class Shower : Store
{
    [SerializeField] private int _count;

    public override void Sale()
    {
        OnSold(_count);
    }
}
