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
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private int _currentLevel;

        public Stats()
        {
            
        }

        public float Speed => _speed;
        public int Capacity => _capacity;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public int CurrentLevel => _currentLevel;
        
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
        
        public void SetNavMeshAgent(NavMeshAgent agent)
        {
            _navMeshAgent = agent;
        }
    }
}