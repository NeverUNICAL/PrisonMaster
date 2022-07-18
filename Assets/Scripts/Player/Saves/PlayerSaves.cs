using System;
using UnityEngine;
using UnityEngine.AI;

namespace Agava.IdleGame.Model
{
    [Serializable]
    public class PlayerSaves : SavedObject<PlayerSaves>
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _stackCapacity;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public PlayerSaves(string guid)
            : base(guid)
        { }

        public void SetSpeed(float value)
        {
            _speed = value;
            Save();
        }
        
        public void SetCapacity(int value)
        {
            _stackCapacity = value;
            Save();
        }
        
        public void SetNavMeshAgent(NavMeshAgent agent)
        {
            _navMeshAgent = agent;
            Save();
        }

        protected override void OnLoad(PlayerSaves loadedObject)
        {
            _speed = loadedObject._speed;
            _stackCapacity = loadedObject._stackCapacity;
            _navMeshAgent = loadedObject._navMeshAgent;
        }
    }
}