using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace Agava.IdleGame.Model
{
    [Serializable]
    public class AssistantsSaves : SavedObject<AssistantsSaves>
    {
        [SerializeField] private AssistantStats _stats;
        
        public AssistantsSaves(string guid)
            : base(guid)
        {
            _stats = new AssistantStats();
        }
        
        public float Speed => _stats.Speed;
        public int StackCapacity => _stats.Capacity;
        public int CapacityLevel => _stats.CapacityLevel;
        public int SpeedLevel => _stats.SpeedLevel;
        public int Count => _stats.Count;
        
        public void SetSpeed(float value)
        {
            _stats.SetSpeed(value);
        }
        
        public void SetCapacity(int value)
        {
            _stats.SetCapacity(value);
        }

        public void SetCapacityLevel(int value)
        {
            _stats.SetCapacityLevel(value);
        }
        
        public void SetCount(int value)
        {
            _stats.SetCount(value);
        }

        public void SetSpeedLevel(int value)
        {
            _stats.SetSpeedLevel(value);
        }

        protected override void OnLoad(AssistantsSaves loadedObject)
        {
            _stats = loadedObject._stats;
        }
    }
}