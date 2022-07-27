using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Money : MonoBehaviour
{
    [SerializeField] private int _reward;
    
    private MoneySpawner _moneySpawner;
    private Collider _collider;
    private Rigidbody _rigidbody;

    private const float MinDelay = 0.3f;
    private const float MaxDelay = 0.55f;
    private const float TargetScale = 0.5f;

    private Coroutine _coroutineInJob;

    public int Reward => _reward;
    private void OnEnable()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnCollected(Transform target)
    {
        MoveToTarget(target);
    }

    public void SetMoneySpawner(MoneySpawner moneySpawner)
    {
        _moneySpawner = moneySpawner;
    }

    private void MoveToTarget(Transform target)
    {
        float randomTime = Random.Range(MinDelay, MaxDelay);

        if (_coroutineInJob != null)
            StopCoroutine(_coroutineInJob);

        _collider.enabled = false;
        _rigidbody.isKinematic = true;

        transform.parent = target;
        transform.DOScale(TargetScale, randomTime / 1.5f);
        
        if(_moneySpawner!=null)
         _moneySpawner.ReduceCount();
        
        transform.DOLocalMove(Vector3.zero, randomTime).OnComplete(() => Destroy(gameObject));
    }

    public void DisableRigidbody(float delay)
    {
        StartCoroutine(Disable(delay));
    }

    private IEnumerator Disable(float delay)
    {
        yield return new WaitForSeconds(delay);

        _rigidbody.isKinematic = true;
    }
}

