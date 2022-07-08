using System;
using Agava.IdleGame;
using UnityEngine;

namespace Assets.Source.Stack
{
    public class HandStateHandler : MonoBehaviour
    {
        [SerializeField] private StackView _stackView;

        public event Action<bool> HandsStateChanged;

        private void OnEnable()
        {
            _stackView.Fill += OnHandFull;
            _stackView.Empty += OnHandEmpty;
        }

        private void OnDisable()
        {
            _stackView.Fill -= OnHandFull;            
            _stackView.Empty -= OnHandEmpty;
        }

        private void OnHandEmpty()
        {
            HandsStateChanged?.Invoke(true);
        }

        private void OnHandFull()
        {
            HandsStateChanged?.Invoke(false);
        }
    }
}
