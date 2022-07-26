using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Agava.IdleGame;

public class MoveToProducerState : AssistantState
{
    private Vector3 _offset = new Vector3(-0.5f, 0, 0);
    private Transform _target;

    private void Update()
    {
        int[] tempArray = new int[Assistant.ProducerItemCreators.Length];

        for (int i = 0; i < tempArray.Length; i++)
        {
            if (Assistant.ProducerItemCreators[i].gameObject.activeInHierarchy == true)
            {
                tempArray[i] = Assistant.ProducerItemCreators[i].ItemsCountView;
            }
        }

        int minValue = tempArray[0];

        for (int i = 0; i < tempArray.Length; i++)
        {
            if (Assistant.ProducerItemCreators[i].gameObject.activeInHierarchy == true)
            {
                if (tempArray[i] > minValue)
                {
                    minValue = tempArray[i];
                }

            }
        }

        for (int i = 0; i < Assistant.ProducerItemCreators.Length; i++)
        {
            if (Assistant.ProducerItemCreators[i].gameObject.activeInHierarchy == true)
            {
                if (minValue == Assistant.ProducerItemCreators[i].ItemsCountView)
                {
                    Transform transform = Assistant.ProducerItemCreators[i].GetComponentInChildren<StackPresenterTrigger>().transform;
                    _target = transform;
                    Assistant.ChangeTargetTransform(transform);

                }
            }
        }

        Assistant.Move(_target.transform.position + _offset);
    }
}
