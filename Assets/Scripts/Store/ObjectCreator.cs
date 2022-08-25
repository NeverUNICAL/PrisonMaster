using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.IdleGame
{
    public class ObjectCreator : ItemCreator
    {
        [SerializeField] private Transform _spawnObjectPosition;
        [SerializeField] private ObjectTransferZone _objectTransferZone;
        [SerializeField] private float _timeBetweenSpawnItem;
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            Timer.Start(_timeBetweenSpawnItem);
        }

        private void OnEnable()
        {
            _objectTransferZone.Transfered += OnTransfered;
            Timer.Completed += OnTimeOver;
        }

        private void OnDisable()
        {
            _objectTransferZone.Transfered -= OnTransfered;
            Timer.Completed -= OnTimeOver;
        }

        protected override void CreateItems(int count, Transform transmittingObject)
        {
            int itemsCount = CalculateCreatedItemsCount(count);

            for (int i = 0; i < itemsCount; i++)
            {
                ItemsCount++;
                var inst = Instantiate(Template, transmittingObject.position, Quaternion.identity);
                StackPresenter.AddToStack(inst.Stackable);
            }

            if (ItemsCount == StackPresenter.Capacity)
            {
                StackIsFull = true;
                //_text.gameObject.SetActive(true);
            }
            else
            {
                Timer.Start(_timeBetweenSpawnItem);
            }

        }

        private void OnTransfered()
        {
            ItemsCount--;

            if (StackIsFull)
            {
                StackIsFull = false;
                Timer.Start(_timeBetweenSpawnItem);
                _text.gameObject.SetActive(false);
            }
        }

        private void OnTimeOver()
        {
            if (StackIsFull == false)
                CreateItems(CreatingItemCount, _spawnObjectPosition);
        }
    }
}