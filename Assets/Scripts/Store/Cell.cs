using ForCreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Store
{
    [SerializeField] private int _count;
    [SerializeField] private CellDoor _door;

    public CellDoor CellDoor => _door;

    public override void Sale()
    {
        OnSold(_count);
    }
}
