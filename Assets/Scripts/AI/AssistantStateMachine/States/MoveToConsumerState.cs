using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Agava.IdleGame;
using System.Linq;

public class MoveToConsumerState : AssistantState
{
    private Transform _target;

    private void Update()
    {
        int[] tempArray = new int[Assistant.ConsumersItemCreators.Length];

        for (int i = 0; i < tempArray.Length; i++)
        {
            if (Assistant.ConsumersItemCreators[i].gameObject.activeInHierarchy == true)
            {
                tempArray[i] = Assistant.ConsumersItemCreators[i].Count;

            }
        }

        int minValue = tempArray[0];

        for (int i = 0; i < tempArray.Length; i++)
        {
            if (Assistant.ConsumersItemCreators[i].gameObject.activeInHierarchy == true)
            {
                if (Assistant.ConsumersItemCreators[i].Count != Assistant.ConsumersItemCreators[i].Capacity)
                {
                    if (tempArray[i] < minValue)
                        minValue = tempArray[i];
                }
                else
                {
                    if (i + 1 > tempArray.Length)
                        minValue = tempArray[0];
                    else
                        minValue = tempArray[i + 1];
                }
            }
        }

        for (int i = 0; i < Assistant.ConsumersItemCreators.Length; i++)
        {
            if (Assistant.ConsumersItemCreators[i].gameObject.activeInHierarchy == true)
            {
                if (minValue == Assistant.ConsumersItemCreators[i].Count)
                {
                    Transform transform = Assistant.ConsumersItemCreators[i].GetComponentInChildren<StackPresenterTrigger>().transform;
                    _target = transform;
                    Assistant.ChangeTargetTransform(transform);
                }
            }
        }

        Assistant.Move(_target.transform.position);
    }
}
