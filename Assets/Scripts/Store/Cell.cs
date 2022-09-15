using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Store
{
    [SerializeField] private int _count;

    public override void Sale()
    {
        OnSold(_count);
    }
}
