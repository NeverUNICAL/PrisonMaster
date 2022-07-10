using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptGoodsTransition : AssistantTransition
{
    private void Update()
    {
        int capacityTarget = StackViewTarget.GetComponentsInChildren<Transform>().Length - 1;
        if (capacityTarget == Capacity)
        {
            NeedTransit = true;
        }
    }
}
