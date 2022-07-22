using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Agava.IdleGame.Model;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Agava.IdleGame
{
    public class AssistantsSavePresenter : GUIDMonoBehaviour
    {
        [Space(10)]
        [SerializeField] private AssistantsShop _shop;
        [SerializeField] private SoftCurrencyHolder _softCurrencyHolder;
        
        private AssistantsSaves _saves;
        
        public int SpeedLevel => _saves.SpeedLevel;
        public float Speed => _saves.Speed;
        public int CapacityLevel => _saves.CapacityLevel;
        public int Capacity => _saves.StackCapacity;
        public int Count => _saves.Count;
        public int Money => _softCurrencyHolder.Value;
        
        public event UnityAction Loaded;
        
        private void Awake()
        {
            _saves = new AssistantsSaves(GUID);
        }

        private void OnEnable()
        {
            _shop.SpeedUpgraded += OnSpeedChanged;
            _shop.CapacityUpgraded += OnCapacityChanged;
            _shop.CountUpgraded += OnCountUpgraded;
        }

        private void OnDisable()
        {
            _shop.SpeedUpgraded -= OnSpeedChanged;
            _shop.CapacityUpgraded -= OnCapacityChanged;
            _shop.CountUpgraded -= OnCountUpgraded;
        }

        private void Start()
        {
            _saves.Load();
            OnLoad();
        }
        
        private void OnSpeedChanged(int level,float value,int price)
        {
            _saves.SetSpeedLevel(level);
            _saves.SetSpeed(value);
            _softCurrencyHolder.Spend(price);
            _saves.Save();
        }

        private void OnCapacityChanged(int level,int value,int price)
        {
            _saves.SetCapacityLevel(level);
            _saves.SetCapacity(value);
            _softCurrencyHolder.Spend(price);
            _saves.Save();
        }

        private void OnCountUpgraded(int count, int price)
        {
            _saves.SetCount(count);
            _softCurrencyHolder.Spend(price);
            _saves.Save();
        }
        
        private void OnLoad()
        {
            {
                Loaded?.Invoke();
            }
        }
    }
}