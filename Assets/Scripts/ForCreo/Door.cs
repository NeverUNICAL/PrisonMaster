using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ForCreo
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private DoorButton[] _doorButtons;
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _targetRotation;
        [SerializeField] private Vector3 _defaultRotation;

        private bool _isOpened = false;

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
            Rotate(_targetRotation, _duration);
            _isOpened = true;
        }

        private void Close()
        {
            Rotate(_defaultRotation, _duration);
            _isOpened = false;
        }

        private void Rotate(Vector3 target, float duration)
        {
            transform.DORotate(target, duration);
        }
    }
}