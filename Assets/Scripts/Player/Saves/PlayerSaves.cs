using System;
using System.Runtime.InteropServices;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace Agava.IdleGame.Model
{
    [Serializable]
    public class PlayerSaves : SavedObject<PlayerSaves>
    {
        [SerializeField] private Stats _stats;
        
        public PlayerSaves(string guid)
            : base(guid)
        {
            _stats = new Stats();
        }
        
        public float Speed => _stats.Speed;
        public int StackCapacity => _stats.Capacity;
        public int AreaMask => _stats.AreaMask;
        public int CurrentLevel => _stats.CurrentLevel;

        public void SetSpeed(float value)
        {
           _stats.SetSpeed(value);
        }
        
        public void SetCapacity(int value)
        {
            _stats.SetCapacity(value);
        }
        
        public void SetAreaMask(int value)
        {
           _stats.SetAreaMask(value);
        }

        public void SetCurrentLevel(int value)
        {
            _stats.SetCurrentLevel(value);
        }

        protected override void OnLoad(PlayerSaves loadedObject)
        {
            _stats = loadedObject._stats;
        }
    }
}