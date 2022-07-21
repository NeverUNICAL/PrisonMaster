using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Agava.IdleGame;
using System.Linq;

public class MoveToConsumerState : AssistantState
{
    private Vector3 _offset;
    private Transform _target;

    private void OnEnable()
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
                if (tempArray[i] < minValue)
                    minValue = tempArray[i];
        }

        for (int i = 0; i < Assistant.ConsumersItemCreators.Length; i++)
        {
            if (Assistant.ConsumersItemCreators[i].gameObject.activeInHierarchy == true)
            {
                if (minValue == Assistant.ConsumersItemCreators[i].Count)
                {
                    _offset = new Vector3(i, 0, i);
                    _target = Assistant.ConsumersItemCreators[i].transform;
                    Assistant.ChangeTargetTransform(Assistant.Consumers[i].transform);
                }
            }
        }

        Assistant.Move(_target.transform.position + _offset);
    }
}
