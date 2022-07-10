using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutGoods : AssistantTransition
{
    private void Update()
    {
        int capacityTarget = StackViewTarget.GetComponentsInChildren<Transform>().Length - 1;
        if (capacityTarget == 0)
        {
            NeedTransit = true;
        }
    }
}
