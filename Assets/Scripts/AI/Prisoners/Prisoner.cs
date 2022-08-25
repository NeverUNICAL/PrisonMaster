using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

namespace ForCreo
{
    public class Prisoner : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private bool _pathEnded;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        public void Rotate(Transform target, float duration = 1f)
        {
            transform.DORotateQuaternion(target.rotation, duration);
        }

        private void FixedUpdate()
        {
            _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
        }

        public bool PathEnded()
        {
            _pathEnded = false;

            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                _pathEnded = true;

            return _pathEnded;
        }

        public void Eating()
        {
            _animator.SetTrigger("IsAngry");
        }
    }
}