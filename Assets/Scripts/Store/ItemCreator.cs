using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.IdleGame.Model;

namespace Agava.IdleGame
{
    public abstract class ItemCreator : MonoBehaviour
    {

        [SerializeField] protected StackPresenter StackPresenter;
        [SerializeField] protected StackableObjectPresenter Template;
        [SerializeField] protected int CreatingItemCount;

        protected Timer Timer = new Timer();
        protected int ItemsCount = 0;
        protected bool StackIsFull = false;
        
        public ITimer ITimer => Timer;
        public bool IStackIsFull => StackIsFull;

        private void Update()
        {
            Timer.Tick(Time.deltaTime);
        }

        protected int CalculateCreatedItemsCount(int count)
        {
            if (ItemsCount + count <= StackPresenter.Capacity)
                return count;
            else
                return StackPresenter.Capacity - ItemsCount;
        }

        protected abstract void CreateItems(int count, Transform transmittingObject);
    }
}