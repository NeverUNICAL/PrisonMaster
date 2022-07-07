using System;
using UnityEngine;
using UnityEngine.AI;
using Assets.Source.Stickmans;

namespace Assets.Source.Player
{
    public class PlayerMover : Mover
    {
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private NavMeshAgent _agent;

        private const float Speed = 0.13f;
        private const float Smooth = 0.2f;

        public NavMeshAgent Agent => _agent;

        public event Action<bool> Stoped;
        public event Action<Vector2, bool> Runnig;

        private void FixedUpdate()
        {
            if (_joystick.Direction == Vector2.zero)
            {
                Stoped?.Invoke(IsEmptyHand);
                return;
            }

            Move();
        }

        private void Move()
        {
            if (_agent.enabled == false)
                return;

            Vector3 direction = GetDirection();

            Rotate(direction);
            _agent.Move(direction);

            Runnig?.Invoke(_joystick.Direction, IsEmptyHand);
        }

        private void Rotate(Vector3 direction)
        {
            Quaternion targetDirection = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetDirection, Smooth);
        }

        private Vector3 GetDirection()
        {
            return new Vector3(_joystick.Horizontal, 0, _joystick.Vertical) * Speed;
        }
    }
}