using System;
using UnityEngine;

    public abstract class Store : MonoBehaviour
    {
        public event Action<int> Sold;

        protected void OnSold(int count)
        {
            Sold?.Invoke(count);
        }
    }

