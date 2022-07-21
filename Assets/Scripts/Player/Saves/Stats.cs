using System;
using UnityEngine;
using UnityEngine.AI;

namespace Agava.IdleGame.Model
{
    [Serializable]
    public class Stats
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _capacity;
        [SerializeField] private int _areaMask;
        [SerializeField] private int _currentLevel;
        [SerializeField] private int _capacityLevel;
        [SerializeField] private int _speedLevel;

        public Stats()
        {
            
        }

        public float Speed => _speed;
        public int Capacity => _capacity;
        public int AreaMask => _areaMask;
        public int CurrentLevel => _currentLevel;
        public int CapacityLevel => _capacityLevel;
        public int SpeedLevel => _speedLevel;
        
        public void SetSpeed(float value)
        {
            _speed = value;
        }

        public void SetCurrentLevel(int value)
        {
            _currentLevel = value;
        }
        
        public void SetCapacity(int value)
        {
            _capacity = value;
        }
        
        public void SetAreaMask(int value)
        {
            _areaMask = value;
        }

        public void SetCapacityLevel(int value)
        {
            _capacityLevel = value;
        }
        
        public void SetSpeedLevel(int value)
        {
            _speedLevel = value;
        }
    }
}