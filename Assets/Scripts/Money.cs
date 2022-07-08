using DG.Tweening;
using UnityEngine;

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Money : MonoBehaviour
    {
        private Collider _collider;
        private Rigidbody _rigidbody;

        private const float MinDelay = 0.1f;
        private const float MaxDelay = 0.2f;
        private const float TargetScale = 0.7f;

        private void OnEnable()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnCollected(Transform target)
        {
            MoveToTarget(target);
        }

        private void MoveToTarget(Transform target)
        {
            float randomTime = Random.Range(MinDelay, MaxDelay);

            _collider.enabled = false;
            _rigidbody.isKinematic = true;

            transform.parent = target;
            transform.DOScale(TargetScale, randomTime / 1.5f);
            transform.DOLocalMove(Vector3.zero, randomTime).OnComplete(() => Destroy(gameObject));
        }
    }

