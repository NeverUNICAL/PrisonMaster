using Agava.IdleGame;
using Agava.IdleGame.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ForCreo
{
    public class TableFood : MonoBehaviour
    {
        [SerializeField] private StorageStackPresenter _storage;
        [SerializeField] private PrisonersQueuePoint[] _points;

        private bool _isFirst = true;

        public PrisonersQueuePoint[] Points => _points;
        public StorageStackPresenter Storage => _storage;

        public event UnityAction<TableFood> Full;

        private void OnEnable()
        {
            _storage.Added += OnAdded;
        }

        private void OnDisable()
        {
            _storage.Added += OnAdded;
        }

        private void OnAdded(StackableObject stackableObject)
        {
            if (_storage.Capacity == _storage.Count && _isFirst)
            {
                Full?.Invoke(this);
                _isFirst = false;
            }
        }
    }
}