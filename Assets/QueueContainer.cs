using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueContainer : QueueHandler
{
    public List<GameObject> PrisonerQueueList => _prisonerList;
    void Start()
    {
        GenerateList();
    }


}
