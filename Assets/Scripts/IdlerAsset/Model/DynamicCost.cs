using System;
using UnityEngine;

namespace Agava.IdleGame.Model
{
    [Serializable]
    public class DynamicCost
    {
        [SerializeField] private readonly int _totalCost;
        [SerializeField] private int _currentCost;
        [SerializeField] private float _currentPercent = 1;

        public DynamicCost(int totalCost)
        {
            if (totalCost < 1)
                throw new ArgumentOutOfRangeException(nameof(totalCost));

            _totalCost = totalCost;
            _currentCost = totalCost;
        }

        public int TotalCost => _totalCost;
        public int CurrentCost => _currentCost;
        public float CurrentPercent => _currentPercent;

        public void Subtract(int value, int totalCost)
        {
            _currentCost -= value;
            _currentPercent = ((float) (_currentCost * 100) / totalCost);
            _currentPercent /= 100;
            
            if (_currentCost < 0)
                _currentCost = 0;
        }
    }
}