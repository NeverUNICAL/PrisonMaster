using System;
using UnityEngine;

    public abstract class Store : MonoBehaviour
    {
        public event Action<int> Sold;
        
        public virtual void Sale() {}

        protected void OnSold(int count)
        {
            Sold?.Invoke(count);
        }
    }

