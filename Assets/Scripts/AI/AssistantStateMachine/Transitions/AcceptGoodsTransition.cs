using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptGoodsTransition : AssistantTransition
{
    private void Update()
    {
        int capacityTarget = Assistant.AssistantStack.GetComponentsInChildren<Transform>().Length - 1;

        Debug.Log(Assistant.Capacity);
        Debug.Log(capacityTarget);
        if (capacityTarget == Assistant.Capacity)
        {
            Debug.Log(Assistant.Capacity);
            NeedTransit = true;
        }
    }
}
