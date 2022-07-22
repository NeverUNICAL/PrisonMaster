using System;
using UnityEngine;
using UnityEngine.AI;

namespace Agava.IdleGame.Model
{
    [Serializable]
    public class AssistantStats
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _capacity;
        [SerializeField] private int _capacityLevel;
        [SerializeField] private int _speedLevel;
        [SerializeField] private int _count;

        public AssistantStats()
        {

        }

        public float Speed => _speed;
        public int Capacity => _capacity;
        public int CapacityLevel => _capacityLevel;
        public int SpeedLevel => _speedLevel;
        public int Count => _count;

        public void SetSpeed(float value)
        {
            _speed = value;
        }

        public void SetCount(int value)
        {
            _count = value;
        }
        
        public void SetCapacity(int value)
        {
            _capacity = value;
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