using Agava.IdleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlocableCellZone : UnlockableObject
{
    [SerializeField] private GameObject _cell;


    public override GameObject Unlock(Transform parent, bool onLoad, string guid)
    {
        _cell.SetActive(true);
        return _cell;
    }
}
