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
        [SerializeField] private LevelBuyZone _thirdLevelBuyZone;
        
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
            _thirdLevelBuyZone.LevelUnlocked += OnLevelUnlocked;
        }

        private void OnDisable()
        {
            _upgrades.SpeedUpgraded -= OnSpeedChanged;
            _upgrades.CapacityUpgraded -= OnCapacityChanged;
            _secondLevelBuyZone.LevelUnlocked -= OnLevelUnlocked;
            _thirdLevelBuyZone.LevelUnlocked -= OnLevelUnlocked;
            
        }

        private void Start()
        {
            _playerSaves.Load();
            OnLoad();
        }
        
        private void ChangeNavMeshAgent(int value)
        {
            _playerSaves.SetAreaMask(value);
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

        private void OnLevelUnlocked(int maskId,int levelNumber)
        {
            if (_playerSaves.CurrentLevel == levelNumber)
            {
                _navMeshAgent.areaMask = maskId; 
                _playerSaves.SetCurrentLevel(levelNumber+1);
                ChangeNavMeshAgent(_navMeshAgent.areaMask);
            }
        }

        private void OnLoad()
        {
            {
                if (_playerSaves.AreaMask != 0)
                    _navMeshAgent.areaMask = _playerSaves.AreaMask;
                else
                    _playerSaves.SetAreaMask(_navMeshAgent.areaMask); 

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