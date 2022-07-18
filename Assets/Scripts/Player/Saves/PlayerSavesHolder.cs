using Agava.IdleGame.Model;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Agava.IdleGame
{
    public class PlayerSavesHolder : MonoBehaviour
    {
        private Currency _currency;
        private PlayerSaves _playerSaves;
        
        private void OnEnable()
        {
            _currency.Load();
        }

        private void OnDisable()
        {
            _currency.Save();
        }

        public void SetSpeed(float value)
        {
            _playerSaves.SetSpeed(value);
        }
        
        public void SetCapacity(int value)
        {
            _playerSaves.SetSpeed(value);
        }
        
        public void SetNavMeshAgent(NavMeshAgent agent)
        {
            _playerSaves.SetNavMeshAgent(agent);
        }
    }
}