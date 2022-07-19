using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Agava.IdleGame.Model;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Agava.IdleGame
{
    public class PlayerSavePresenter : GUIDMonoBehaviour
    {
        [Space(10)]
        [SerializeField] private float _speed;
        [SerializeField] private StackPresenter _stackPresenter;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Upgrades _upgrades;
        [SerializeField] private LevelBuyZone _secondLevelBuyZone;
        
        private PlayerSaves _playerSaves;
        
        private void Awake()
        {
            _playerSaves = new PlayerSaves(GUID);
        }

        private void OnEnable()
        {
            _upgrades.SpeedUpgraded += OnSpeedChanged;
            _upgrades.CapacityUpgraded += OnCapacityChanged;
            _secondLevelBuyZone.LevelUnlocked += OnLevelUnlocked;
        }

        private void OnDisable()
        {
            _upgrades.SpeedUpgraded -= OnSpeedChanged;
            _upgrades.CapacityUpgraded -= OnCapacityChanged;
        }

        private void Start()
        {
            _playerSaves.Load();
            OnLoad();
        }
        
        private void ChangeNavMeshAgent(NavMeshAgent agent)
        {
            _playerSaves.SetNavMeshAgent(agent);
            _playerSaves.Save();
        }

        private void OnSpeedChanged(float value)
        {
            _playerSaves.SetSpeed(value);
            _playerSaves.Save();
        }
        
        private void OnCapacityChanged(int value)
        {
            _playerSaves.SetCapacity(value);
            _playerSaves.Save();
        }

        private void OnLevelUnlocked(NavMeshAgent navMeshAgent,int levelNumber)
        {
            if (_playerSaves.CurrentLevel == levelNumber)
            {
                _navMeshAgent.areaMask = navMeshAgent.areaMask;
                _playerSaves.SetCurrentLevel(levelNumber++);
                ChangeNavMeshAgent(_navMeshAgent);
            }
        }

        private void OnLoad()
        {
            {
               if (_playerSaves.NavMeshAgent != null)
                   _navMeshAgent = _playerSaves.NavMeshAgent;
               else
                _playerSaves.SetNavMeshAgent(_navMeshAgent);

               if(_playerSaves.StackCapacity != 0)
                _stackPresenter.ChangeCapacity(_playerSaves.StackCapacity);
               else
                _playerSaves.SetCapacity(_stackPresenter.Capacity);
               
               
               if(_playerSaves.Speed != 0)
                   _navMeshAgent.speed = _playerSaves.Speed;
               else
                _playerSaves.SetSpeed(_navMeshAgent.speed);
               
               if(_playerSaves.CurrentLevel == 0)
                   _playerSaves.SetCurrentLevel(1);
            }
        }
        
    }
}