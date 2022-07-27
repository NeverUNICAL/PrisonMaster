using System.Collections;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] private Store _store;
    [SerializeField] private Money _moneyTemplate;
    [SerializeField] private Transform[] _points;
    [SerializeField] private int _maxCount;

    private int _currentCount;
    private int _counter = 0;
    private const float Delay = 0.2f;
    private float _delayForStopMoney = 2f;

    private void OnEnable()
    {
        _store.Sold += Spawn;
    }

    private void OnDisable()
    {
        _store.Sold -= Spawn;
    }

    public void ReduceCount()
    {
        _currentCount--;
    }

    private void Spawn(int count)
    {
        StartCoroutine(MoneyGenerator(count));
    }

    private IEnumerator MoneyGenerator(int count)
    {
        var delay = new WaitForSeconds(Delay);

        for (int i = 0; i < count * 2; i++)
        {
            yield return delay;
            if (_counter > _points.Length - 1)
                _counter = 0;

            if (_currentCount < _maxCount)
            {
                Money money = Instantiate(_moneyTemplate, _points[_counter].position, transform.rotation, transform);
                money.SetMoneySpawner(this);
                _currentCount++;
                _counter++;
                money.DisableRigidbody(_delayForStopMoney);
            }
        }
    }
}

