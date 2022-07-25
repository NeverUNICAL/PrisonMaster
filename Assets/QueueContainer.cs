using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using UnityEngine;

public class QueueContainer : QueueHandler
{
    [SerializeField] private Distributor _distributor;
    [SerializeField] private Shop _shop;
        
    private void Start()
    {
        GenerateList();
        StartCoroutine(TryToSendPrisoner());
    }

    private IEnumerator TryToSendPrisoner()
    {
        while (true)
        {
            if (_prisonerList.Count > 0 )
            {
                if (_shop.Count > 2 && _prisonerList[0].PathEnded()) 
                {
                   
                    if(SendToPool(_distributor))
                        _shop.Sale();
                }
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
