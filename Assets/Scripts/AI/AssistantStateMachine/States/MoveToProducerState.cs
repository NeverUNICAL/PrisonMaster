using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToProducerState : AssistantState
{
    [SerializeField] private Vector3 _offset = new Vector3(-2.5f, 0, 0);
    private Transform _target;

    private void Start()
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
                if (tempArray[i] > minValue)
                    minValue = tempArray[i];
        }

        for (int i = 0; i < Assistant.ProducerItemCreators.Length; i++)
        {
            if (Assistant.ProducerItemCreators[i].gameObject.activeInHierarchy == true)
            {
                if (minValue == Assistant.ProducerItemCreators[i].ItemsCountView)
                {
                    _target = Assistant.ProducerItemCreators[i].transform;
                    Assistant.ChangeTargetTransform(Assistant.Producers[i].transform);
                }
            }
        }

        Assistant.Move(_target.transform.position + _offset);
    }
}
