using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private PositionHandler[] _positionHandlers;

    private void Awake()
    {
        _positionHandlers = new PositionHandler[transform.childCount];

        for (int i = 0; i < _positionHandlers.Length; i++)
        {
            _positionHandlers[i] = transform.GetChild(i).GetComponent<PositionHandler>();
        }
    }
}
