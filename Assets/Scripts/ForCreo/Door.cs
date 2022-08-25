using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace ForCreo
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private DoorButton[] _doorButtons;
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _targetRotation;
        [SerializeField] private Vector3 _defaultRotation;

        private bool _isOpened = false;

        public bool IsOpened => _isOpened;

        public event UnityAction Opened;
        public event UnityAction Closed;

        private void OnEnable()
        {
            for (int i = 0; i < _doorButtons.Length; i++)
                _doorButtons[i].Reached += OnReached;
        }

        private void OnDisable()
        {
            for (int i = 0; i < _doorButtons.Length; i++)
                _doorButtons[i].Reached -= OnReached;
        }

        private void OnReached()
        {
            if (_isOpened == false)
                Open();
            else
                Close();
        }

        private void Open()
        {
            Rotate(_targetRotation, _duration, Opened);
            _isOpened = true;
        }

        private void Close()
        {
            Rotate(_defaultRotation, _duration, Closed);
            _isOpened = false;
        }

        private void Rotate(Vector3 target, float duration, UnityAction action)
        {
            transform.DORotate(target, duration).OnComplete(() => action?.Invoke());
        }
    }
}