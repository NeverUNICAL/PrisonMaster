using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private StackPresenter _stackPresenter;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private UpgradesShop _upgradesShop;
        [SerializeField] private LevelBuyZone _secondLevelBuyZone;
        [SerializeField] private LevelBuyZone _thirdLevelBuyZone;
        [SerializeField] private SoftCurrencyHolder _softCurrencyHolder;
        
        private PlayerSaves _playerSaves;

        public int Money => _softCurrencyHolder.Value;
        public int SpeedLevel => _playerSaves.SpeedLevel;
        public int CapacityLevel => _playerSaves.CapacityLevel;
        public bool IsTutorialCompleted => _playerSaves.IsTutorialCompleted;
        
        public event UnityAction Loaded;
        
        private void Awake()
        {
            _playerSaves = new PlayerSaves(GUID);
        }

        private void OnEnable()
        {
            _upgradesShop.SpeedUpgraded += OnSpeedChanged;
            _upgradesShop.CapacityUpgraded += OnCapacityChanged;
            _secondLevelBuyZone.LevelUnlocked += OnLevelUnlocked;
            _thirdLevelBuyZone.LevelUnlocked += OnLevelUnlocked;
        }

        private void OnDisable()
        {
            _upgradesShop.SpeedUpgraded -= OnSpeedChanged;
            _upgradesShop.CapacityUpgraded -= OnCapacityChanged;
            _secondLevelBuyZone.LevelUnlocked -= OnLevelUnlocked;
            _thirdLevelBuyZone.LevelUnlocked -= OnLevelUnlocked;
        }

        private void Start()
        {
            _playerSaves.Load();
            OnLoad();
        }

        public void SetTutorialComplete()
        {
            if (_playerSaves.IsTutorialCompleted == false)
            {
                _playerSaves.SetTutorialComplete();
                _playerSaves.Save();
                AppMetrica.Instance.ReportEvent("level_complete", new Dictionary<string, object>()
                {
                    {"level", 1},
                });
                AppMetrica.Instance.SendEventsBuffer();
            }
        }
        
        private void ChangeNavMeshAgent(int value)
        {
            _playerSaves.SetAreaMask(value);
            _playerSaves.Save();
        }

        private void OnSpeedChanged(int level,float value,int price)
        {
            _playerSaves.SetSpeedLevel(level);
            _playerSaves.SetSpeed(value);
            _navMeshAgent.speed = value;
            _softCurrencyHolder.Spend(price);
            _playerSaves.Save();
        }
        
        private void OnCapacityChanged(int level,int value,int price)
        {
            _playerSaves.SetCapacityLevel(level);
            _playerSaves.SetCapacity(value);
            _stackPresenter.ChangeCapacity(value);
            _softCurrencyHolder.Spend(price);
            _playerSaves.Save();
        }

        private void OnLevelUnlocked(int maskId,int levelNumber)
        {
            if (_playerSaves.CurrentLevel == levelNumber)
            {
                _navMeshAgent.areaMask = maskId; 
                _playerSaves.SetCurrentLevel(levelNumber+1);
                ChangeNavMeshAgent(_navMeshAgent.areaMask);
                
                AppMetrica.Instance.ReportEvent("level_complete", new Dictionary<string, object>()
                {
                    {"level", _playerSaves.CurrentLevel},
                });
                AppMetrica.Instance.SendEventsBuffer();
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
                
                Loaded?.Invoke();
            }
        }
    }
}