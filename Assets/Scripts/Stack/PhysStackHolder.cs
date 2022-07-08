using System.Collections.Generic;
using Agava.IdleGame;
using Agava.IdleGame.Model;
using UnityEngine;

    public class PhysStackHolder : MonoBehaviour
    {
        [SerializeField] private StackView _stackView;
        [SerializeField] private Transform _viewPoint;
        [SerializeField] private Transform _parentRotation;

        private IReadOnlyList<StackableObject> _stack => _stackView.Stackables;
        private float _lerpSpeed;

        private const float Offset = 0.5f;
        private const float LerpRotate = 0.2f;
        private const float LerpSpeedHigh = 0.87f;
        private const float LerpSpeedPreMedium = 0.88f;
        private const float LerpSpeedMedium = 0.89f;
        private const float LerpSpeedPreLow = 0.91f;
        private const float LerpSpeedLow = 0.93f;
        private const float LerpSpeedPreSolid = 0.95f;
        private const float LerpSpeedSolid = 0.98f;

        private void Awake()
        {
            _lerpSpeed = LerpSpeedHigh;
        }

        private void FixedUpdate()
        {
            TryIncreaseLerpSpeed();
            Move(_stack);
        }

        private void Move(IReadOnlyList<StackableObject> stack)
        {
            for (int i = 0; i < stack.Count; i++)
            {
                float moveLerp = Mathf.SmoothStep(0, 1, Mathf.Pow(_lerpSpeed, i));
                float rotationLerp = LerpRotate * i;

                Vector3 previousPosition;
                Vector3 currentPosition = stack[i].View.position;
                Quaternion defaultRotation = _parentRotation.rotation;

                if (i == 0)
                    previousPosition = _viewPoint.position;
                else
                    previousPosition = stack[i - 1].View.position;

                Quaternion targetRotation = _viewPoint.rotation;
                Vector3 targetPosition =
                    new Vector3(previousPosition.x, previousPosition.y+ Offset, previousPosition.z);
                stack[i].View.SetPositionAndRotation(Vector3.Lerp(currentPosition, targetPosition, moveLerp),
                    targetRotation);
            }
        }

        private void TryIncreaseLerpSpeed()
        {
            if (_stack.Count < 5)
                _lerpSpeed = LerpSpeedHigh;
            else if (_stack.Count < 8)
                _lerpSpeed = LerpSpeedPreMedium;
            else if (_stack.Count < 12)
                _lerpSpeed = LerpSpeedMedium;
            else if (_stack.Count < 15)
                _lerpSpeed = LerpSpeedPreLow;
            else if (_stack.Count < 18)
                _lerpSpeed = LerpSpeedLow;
            else if (_stack.Count < 21)
                _lerpSpeed = LerpSpeedPreSolid;
            else if (_stack.Count < 24)
                _lerpSpeed = LerpSpeedSolid;
        }
    }
