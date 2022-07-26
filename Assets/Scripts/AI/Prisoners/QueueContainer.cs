using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using UnityEngine;

public class QueueContainer : QueueHandler
{
    [SerializeField] private Distributor _distributor;

    private void Start()
    {
        GenerateList();
        StartCoroutine(TryToSendPrisoner());
    }

    public bool CheckForShopBuyed()
    {
        if (_shop.gameObject.activeInHierarchy)
            return true;
        
        return false;
    }

    private IEnumerator TryToSendPrisoner()
    {
        while (true)
        {
            if (_prisonerList.Count > 0 && _shop.gameObject.activeInHierarchy) 
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
