using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutGoods : AssistantTransition
{
    private void Update()
    {
        int capacityTarget = Assistant.AssistantStack.GetComponentsInChildren<Transform>().Length - 1;
        if (capacityTarget == 0)
        {
            NeedTransit = true;
        }
        else
        {
            NeedAlternativeTransit = true;
        }
    }
}
