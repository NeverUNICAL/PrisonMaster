using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Money : MonoBehaviour
{
    [SerializeField] private int _reward;

    private Transform _moneyConteiner;
    private MoneySpawner _moneySpawner;
    private Collider _collider;
    private Rigidbody _rigidbody;

    private Vector3 _defaultScale;

    private const float MinDelay = 0.3f;
    private const float MaxDelay = 0.55f;
    private const float TargetScale = 0.5f;

    private Coroutine _coroutineInJob;

    public int Reward => _reward;

    private void OnEnable()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _defaultScale = transform.localScale;
    }

    private IEnumerator Disable(float delay)
    {
        yield return new WaitForSeconds(delay);

        _rigidbody.isKinematic = true;
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
        
        transform.DOLocalMove(Vector3.zero, randomTime).OnComplete(ResetState);
    }

    private void ResetState()
    {
        gameObject.SetActive(false);
        transform.parent = _moneyConteiner.transform;
        transform.localScale = _defaultScale;
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
    }
        
    public void DisableRigidbody(float delay)
    {
        StartCoroutine(Disable(delay));
    }

    public void OnCollected(Transform target)
    {
        MoveToTarget(target);
    }

    public void SetMoneySpawner(MoneySpawner moneySpawner)
    {
        _moneySpawner = moneySpawner;
    }

    public void SetConteiner(Transform target)
    {
        _moneyConteiner = target;
    }
}

