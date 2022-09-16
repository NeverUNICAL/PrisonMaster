using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothOnExit : Store
{
    [SerializeField] private int _count;

    public override void Sale()
    {
        OnSold(_count);
        Debug.Log("SaleOnStore");
    }
}
